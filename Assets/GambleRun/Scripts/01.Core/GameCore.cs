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
        private GameTerminator _terminator;

        private void Awake()
        {
            _serializer = gameObject.GetComponent<GameSerializer>();
            _dataBinder = gameObject.GetComponent<GameDataBinder>();
            _terminator = gameObject.GetComponent<GameTerminator>();
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