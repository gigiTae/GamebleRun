using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using GambleRun.UI;
using GambleRun.Props;
using UnityEngine.SceneManagement;

namespace GambleRun.Core
{
    /// <summary>
    /// 게임의 초기화를 담당하는 클래스
    /// </summary>
    public class GameInitiator : MonoBehaviour
    {
        [SerializeField] private GameInitData _initData;

        private LoadingScreenView _loadingScreenView;
        private GameCore _gameCore;
        private GameObject _player;

        private async void Start()
        {
            try
            {
                await StartGameFlow(destroyCancellationToken);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("게임 시작 흐름이 취소되었습니다");
            }
        }

        private async Task StartGameFlow(CancellationToken token)
        {
            ViewLoadScreen();

            await Initialize(token);

            ////////////////// Instantiate Sector ////////////////
            //** Instanisate -> Awake(), OnEable() 호출 **/
            InstantiateObject();


            //////////////// Preparation Sector ///////////////
            _gameCore.Initialize(_initData.Mode);


            // TODO : 카메라 처리하는 클래스 추가

            if (_initData.Mode == GameMode.InGame)
            {
                BindCamera();
            }

            System.GC.Collect();
            /////////////// End Loading ///////////////////////
            _loadingScreenView.CloseLoadingScreen();

        }

        private void ViewLoadScreen()
        {
            _loadingScreenView = Instantiate(_initData.LoadingScreen).GetComponent<LoadingScreenView>();
        }

        private void InstantiateObject()
        {
            // GameCore
            _gameCore = Instantiate(_initData.GameCore).GetComponent<GameCore>();

            // Level
            Instantiate(_initData.Level);

            // Player
            _player = Instantiate(_initData.Player);
        }

        private async Awaitable Initialize(CancellationToken token)
        {
            await Awaitable.WaitForSecondsAsync(0.1f, token);
        }

        private void BindCamera()
        {
            CameraFollow camera = Camera.main.GetComponent<CameraFollow>();

            if (camera != null)
            {
                camera.SetTarget(_player.transform);
            }
            else
            {
                Debug.LogWarning("씬에서 카메라가 없습니다");
            }
        }

    }
}