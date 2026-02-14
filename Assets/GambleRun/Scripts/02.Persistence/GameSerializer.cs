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
        private IDataService _dataService;
        private void Awake()
        {
            _dataService = new FileDataService(new JsonSerializer());
        }

        public void SaveGame(string FileName, GameData gameData)
        {
            _dataService.Save<GameData>(FileName, gameData);
        }

        public GameData LoadGamData(string fileName)
        {
            return _dataService.Load<GameData>(fileName);
        }
    }

}