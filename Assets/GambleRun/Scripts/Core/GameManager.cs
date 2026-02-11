using UnityEngine;


namespace GambleRun.Core
{
    /// <summary>
    /// 게
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] CameraFollow _cameraFollow;
        [SerializeField] GameObject _playerPrefab;
        [SerializeField] PlayerSpawnPoint _spawnPoint;

        private GameObject _player;

        void Awake()
        {
            StartGame();
        }

        private void StartGame()
        {
            if (_spawnPoint != null)
            {
                PointContext pointContext = _spawnPoint.GetRandomPointContext();
                _player = Instantiate(_playerPrefab, pointContext.Position, pointContext.Rotation);
                _cameraFollow.SetTarget(_player.transform);
            }

            Debug.Log("StartGame");
        }

        private void EndGame()
        {
            Debug.Log("EndGame");
        }
    }
}