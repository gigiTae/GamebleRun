using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace GambleRun
{
    public class PlayerAnimation : MonoBehaviour
    {
        private readonly int MOVE_SPEED_ID = Animator.StringToHash("MoveSpeed");
        private readonly int ON_INTERACTION_ID = Animator.StringToHash("OnInteraction");

        private Animator _animator;
        private PlayerMovement _movement;

        [SerializeField] private LootEvent _lootOpenEvent;
        [SerializeField] private LootEvent _lootCloseEvent;

        private void Awake()
        {
            _movement = GetComponentInParent<PlayerMovement>();

            if (_movement == null)
            {
                Debug.Log("PlayertMovement is null");
            }

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

        // Update is called once per frame
        private void Update()
        {
            UpdateAnimParams();
        }

        private void UpdateAnimParams()
        {
            if (_animator != null)
            {
                _animator.SetFloat(MOVE_SPEED_ID, _movement.HorizontalSpeedRatio);
            }
        }
    }
}