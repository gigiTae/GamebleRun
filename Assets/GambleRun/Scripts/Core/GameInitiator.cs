using GambleRun.Core;
using GambleRun.Persistence;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


namespace GambleRun
{
    /// <summary>
    /// 게임의 초기화를 담당하는 클래스
    /// </summary>
    public class GameInitiator : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject _loadingScreenPrefab;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _levelPrefab;

        private LoadingScreenView _loadingScreenView;

        private async void Start()
        {
            try
            {
                await StartGameFlow(destroyCancellationToken);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("게임 시작 흐름이 취소되었습니다 (오브젝트 파괴 등).");
            }
        }

        private async Task StartGameFlow(CancellationToken token)
        {
            ViewLoadScreen();

            await Initialize(token);


            ////////////////// Creation Sector ////////////////

            CreateLevel();
            var player = Instantiate(_playerPrefab);

            //** Instanisate -> Awake(), OnEable() 호출 **/

            //////////////// Preparation Sector ///////////////

            // LoadGameData

            // New or Load

            // ApplyGameData
            SaveLoadManager.Instance.ApplyGameData();


            // CameraBind
            Camera.main.GetComponent<CameraFollow>().SetTarget(player.transform);

            //SaveLoadManager.Instance;

            _loadingScreenView.CloseLoadingScreen();

            // 함수 종료시 Start() 호출
        }

        private void ViewLoadScreen()
        {
            _loadingScreenView = Instantiate(_loadingScreenPrefab).GetComponent<LoadingScreenView>();
        }

        private void CreateLevel()
        {
            Instantiate(_levelPrefab);
        }

        private async Awaitable Initialize(CancellationToken token)
        {
            await Awaitable.WaitForSecondsAsync(0.1f, token);
        }

    }
}