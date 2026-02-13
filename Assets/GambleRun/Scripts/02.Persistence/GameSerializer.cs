using GambleRun.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using GambleRun.Player;
using UnityEngine.UIElements;

namespace GambleRun.Persistence
{
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
    public class GameSerializer : MonoBehaviour
    {
        [SerializeField] GameData _gameData = new();

        public GameData GameData => _gameData;

        private IDataService _dataService;
        private void Awake()
        {
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
                LoadGamData("Save");
            }
        }

        public void SaveGame() => _dataService.Save<GameData>("dd", _gameData);

        public GameData LoadGamData(string fileName)
        {
            return _dataService.Load<GameData>(fileName);
        }

        //public void ReloadGame() => LoadGame(_gameData.FileName);

        public void DeleteGame(string gameName) => _dataService.Delete(gameName);

    }

}