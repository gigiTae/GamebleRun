using GambleRun.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


namespace GambleRun.Persistence
{

    [Serializable]
    public class GameData
    {
        public string FileName = "Save";
        public string CurrentLevelName = "S";

        public PlayerData Player;
    }

    public interface ISaveable
    {
        SerializableGuid Id { get; set; }
    }

    public interface IBind<TData> where TData : ISaveable
    {
        SerializableGuid Id { get; set; }
        void Bind(TData data);
    }

    /// <summary>
    /// 게임의 로드와 세이브를 담당합니다
    /// </summary>
    public class SaveLoadManager : Singleton<SaveLoadManager>
    {
        [SerializeField] GameData _gameData =new();

        private IDataService _dataService;
        protected override void Awake()
        {
            base.Awake();
            _dataService = new FileDataService(new JsonSerializer());
        }

        public void Update()
        {
            if (Keyboard.current.zKey.isPressed)
            {
                Debug.Log("SaveGame");
                SaveGame();
            }

            if (Keyboard.current.xKey.isPressed)
            {
                Debug.Log("LoadGame");
                LoadGame(_gameData.FileName);
                ApplyGameData();
            }
        }

        public void ApplyGameData()
        {
            Bind<Player, PlayerData>(_gameData.Player);

            // Inventory

            // Store
        }

        void Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
        {
            var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
            if (entity != null)
            {
                if (data == null)
                {
                    data = new TData { Id = entity.Id };
                }
                entity.Bind(data);
            }
        }

        void Bind<T, TData>(List<TData> datas) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
        {
            var entities = FindObjectsByType<T>(FindObjectsSortMode.None);

            foreach (var entity in entities)
            {
                var data = datas.FirstOrDefault(d => d.Id == entity.Id);
                if (data == null)
                {
                    data = new TData { Id = entity.Id };
                    datas.Add(data);
                }
                entity.Bind(data);
            }
        }
        public void SaveGame() => _dataService.Save(_gameData);
        public void LoadGame(string fileName)
        {
            _gameData = _dataService.Load(fileName);

            if (String.IsNullOrWhiteSpace(_gameData.CurrentLevelName))
            {
                _gameData.CurrentLevelName = "Demo";
            }
        }

        public void ReloadGame() => LoadGame(_gameData.FileName);

        public void DeleteGame(string gameName) => _dataService.Delete(gameName);

    }

}