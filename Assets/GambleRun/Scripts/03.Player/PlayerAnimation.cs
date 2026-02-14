using UnityEngine;
using GambleRun.Event;
using GambleRun.Storages;

namespace GambleRun.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private readonly int MOVE_SPEED_ID = Animator.StringToHash("MoveSpeed");
        private readonly int ON_INTERACTION_ID = Animator.StringToHash("OnInteraction");

        [SerializeField] private PlayerSettingData _settingData;
        private PlayerData _data;
        public void SetPlayerData(PlayerData playerData)
        {
            _data = playerData;
        }

        private Animator _animator;

        [SerializeField] private LootEvent _lootOpenEvent;
        [SerializeField] private LootEvent _lootCloseEvent;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            if (_animator == null)
            {
                Debug.Log("Animator is null");
            }

            if (_lootOpenEvent != null)
            {
                _lootOpenEvent.Subscribe(OnInteraction);
            }
            else
            {
                Debug.Log("ItmeBoxOpenEvent is null");
            }

            if (_lootCloseEvent != null)
            {
                _lootCloseEvent.Subscribe(OffInteraction);
            }
            else
            {
                Debug.Log("ItmeBoxCloseEvent is null");
            }
        }

        private void OnDestroy()
        {
            _lootOpenEvent.Unsubscribe(OnInteraction);
            _lootCloseEvent.Unsubscribe(OffInteraction);
        }

        private void OnInteraction(StorageData data)
        {
            _animator.SetBool(ON_INTERACTION_ID, true);
        }

        private void OffInteraction(StorageData data)
        {
            _animator.SetBool(ON_INTERACTION_ID, false);
        }

        private void Update()
        {
            UpdateAnimParams();
        }
        private float HorizontalSpeedRatio
        {
            get
            {
                Vector3 horizontalVelocity = new Vector3(_data.CurrentVelocity.x, 0, _data.CurrentVelocity.z);
                return horizontalVelocity.magnitude / _settingData.SprintSpeed;
            }
        }

        private void UpdateAnimParams()
        {
            if (_animator != null)
            {
                _animator.SetFloat(MOVE_SPEED_ID, HorizontalSpeedRatio);
            }
        }
    }
}