using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GambleRun.Core
{
    [System.Serializable]
    public struct PointContext
    {
        public Vector3 Position;
        public Vector3 RotationEuler;

        public Quaternion Rotation => Quaternion.Euler(RotationEuler);
    }

    /// <summary>
    /// PlayerSpawn Point
    /// </summary>
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private List<PointContext> _spawnPoints = new();

        public List<PointContext> Points => _spawnPoints;

        public PointContext GetRandomPointContext()
        {
            if (_spawnPoints == null || _spawnPoints.Count == 0)
            {
                Debug.LogWarning("SpawnPoints 리스트가 비어있습니다! 기본값을 반환합니다.");
                return default; // 모든 값이 0인 구조체 반환
            }

            int index = Random.Range(0, _spawnPoints.Count);
            return _spawnPoints[index];
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            DrawPoints();
        }

        private void DrawPoints()
        {
            for (int i = 0; i < _spawnPoints.Count; i++)
            {
                Gizmos.color = new Color(0, 1, 0, 0.5f);
                Gizmos.DrawSphere(_spawnPoints[i].Position, 0.25f);

                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(_spawnPoints[i].Position, 0.25f);

                // 시선벡터
                Vector3 fowardDirection = _spawnPoints[i].Rotation * Vector3.forward;
                Gizmos.color = new Color(0, 1, 0, 0.5f);
                Gizmos.DrawRay(_spawnPoints[i].Position, fowardDirection * 1.0f);

                GUIStyle style = new GUIStyle();
                style.normal.textColor = Color.yellow; // 텍스트 색상
                style.fontStyle = FontStyle.Bold;      // 굵게

                string label = $"[PlayerSpawnPoint {i}] ";

                Handles.Label(_spawnPoints[i].Position + Vector3.up * 0.7f, label, style);
            }
        }
#endif

    }

}