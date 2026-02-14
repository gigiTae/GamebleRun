using GambleRun.CameraControl;
using GambleRun.Event;
using GambleRun.Persistence;
using GambleRun.Storages;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GambleRun.Core
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField] GameCoreData _gameCoreData;

        private GameData _gameData;

        private GameSerializer _serializer;
        private GameDataBinder _dataBinder;

        // Events
        [SerializeField] private RequestStartEvent _startEvent;
        [SerializeField] private RequestExitEvent _exitEvent;

        private void Awake()
        {
            if (_gameCoreData == null)
            {
                Debug.LogWarning("GameCoreData가 없습니다");
            }

            _serializer = gameObject.GetComponent<GameSerializer>();
            _dataBinder = gameObject.GetComponent<GameDataBinder>();
        }

        private void OnEnable()
        {
            _startEvent.Subscribe(OnRequestStart);
            _exitEvent.Subscribe(OnRequestExit);
        }


        private void OnDisable()
        {
            _startEvent.Unsubscribe(OnRequestStart);
            _exitEvent.Unsubscribe(OnRequestExit);
        }

        public void Initialize(GameMode mode)
        {
            // 1. LoadGameData
            _gameData = _serializer.LoadGamData(_gameCoreData.SaveFileName);

            bool isNewGame = false;

            // 세이브 데이터가 없는경우 
            if (_gameData == null)
            {
                Debug.Log("세이브 파일이 없어서 새로 만듭니다");
                _gameData = new GameData();
                isNewGame = true;
            }

            // 2. BindGameData
            _dataBinder.BindGameData(mode, _gameData);

            // 3. 기본 스토리정보 추가
            if (isNewGame)
            {
                SetupNewGame();
                _dataBinder.BindGameData(mode, _gameData);
            }
        }

        public void OnRequestStart()
        {
            Debug.Log("Start");

            // -NEW GAME
            // 1. 게임 정보 저장 


            // 2. 씬 변경
            // -LOAD GAME


            SceneManager.LoadScene(_gameCoreData.InGameScene);
        }

        public void OnRequestExit()
        {
            // TODO : 게임 종료 처리
            _serializer.SaveGame(_gameCoreData.SaveFileName, _gameData);


#if UNITY_EDITOR
            // 유니티 에디터의 플레이 모드를 종료
            EditorApplication.isPlaying = false;
#else
            // 실제 빌드된 게임 프로그램 종료
            Application.Quit();
#endif
        }

        public void SetupNewGame()
        {
            // 1. Store 
            foreach (StorageData storage in _gameData.PersistanceData.Storages)
            {
                switch (storage.Type)
                {
                    case Storages.StorageType.Backpack:
                        storage.ResetStorage(_gameCoreData.GameeConfig.BackpackCapacity);
                        break;
                    case Storages.StorageType.Store:
                        storage.ResetStorage(_gameCoreData.GameeConfig.StoreCapacity);
                        break;
                    case Storages.StorageType.Equipment:
                        storage.ResetStorage(_gameCoreData.GameeConfig.EquipmentCapacity);
                        break;
                }
            }
        }
    }
}