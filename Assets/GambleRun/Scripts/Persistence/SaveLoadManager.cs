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
        public List<StorageData> Storages;
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
        [SerializeField] GameData _gameData = new();

        private IDataService _dataService;
        protected override void Awake()
        {
            base.Awake();
            _dataService = new FileDataService(new JsonSerializer());
        }

        public void Update()
        {

            if (Keyboard.current.zKey.wasPressedThisFrame)
            {
                Debug.Log("SaveGame");
                SaveGame();
            }

            if (Keyboard.current.xKey.wasPressedThisFrame)
            {
                Debug.Log("LoadGame");
                LoadGame(_gameData.FileName);
                ApplyGameData();
            }

        }

        public void ApplyGameData()
        {
            _gameData.Player = Bind<Player, PlayerData>(_gameData.Player);

            _gameData.Storages = Bind<Storage, StorageData>(_gameData.Storages);
        }

        TData Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
        {
            var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
            if (entity != null)
            {
                // ** 전달값이 null이면 참조가 아니기때문에 원본객체를 수정할 수 없기때문에
                // ** new() 할당 받은 객체에 리턴값을 통해서 원복객체에 전달합니다
                // ** ref 방법도 있음
                if (data == null)
                {
                    data = new TData { Id = entity.Id };
                }
                entity.Bind(data);
            }

            return data;
        }

        List<TData> Bind<T, TData>(List<TData> datas) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
        {
            var entities = FindObjectsByType<T>(FindObjectsSortMode.None);

            foreach (var entity in entities)
            {
                if (datas == null)
                {
                    datas = new List<TData>();
                }

                var data = datas.FirstOrDefault(d => d.Id == entity.Id);
                if (data == null)
                {
                    data = new TData { Id = entity.Id };
                    datas.Add(data);
                }
                entity.Bind(data);
            }

            return datas;
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