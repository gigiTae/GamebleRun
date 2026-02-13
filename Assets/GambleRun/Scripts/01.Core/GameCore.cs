using GambleRun.CameraControl;
using GambleRun.Persistence;
using UnityEngine;

namespace GambleRun.Core
{

    public class GameCore : MonoBehaviour
    {
        private GameData _gameData;

        private GameSerializer _serializer;
        private GameDataBinder _dataBinder;
        private void Awake()
        {
            _serializer = gameObject.GetComponent<GameSerializer>();
            _dataBinder = gameObject.GetComponent<GameDataBinder>();
        }

        public void Initialize(GameMode mode, string saveFileName)
        {
            // 1. LoadGameData
            _gameData = _serializer.LoadGamData(saveFileName);

            // 세이브 데이터가 없는경우 
            if (_gameData == null)
            {
                _gameData = new GameData();
            }

            // 2. BindGameData
            _dataBinder.BindGameData(mode, _gameData);
        }



    }
}