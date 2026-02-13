using UnityEngine;

using GambleRun.Config;

namespace GambleRun.Manager
{
    // 게임의 설정을 관리하는 클래스
    public class ConfigManager : MonoBehaviour
    {
        private static ConfigManager _instance;

        public static ConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<ConfigManager>();
                    if (_instance == null)
                    {
                        GameObject container = new GameObject("ConfigManager");
                        _instance = container.AddComponent<ConfigManager>();
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            // 씬 전환 시 파괴되지 않도록 설정
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                // 중복 생성된 경우 파괴
                Destroy(gameObject);
            }
        }

        [SerializeField] private SlotConfig _slotConfig;
        public SlotConfig SlotConfig => _slotConfig;

    }
}