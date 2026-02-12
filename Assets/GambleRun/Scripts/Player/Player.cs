using GambleRun.Persistence;
using System;
using UnityEngine;

namespace GambleRun
{
    // 플레이어 공유데이터
    [Serializable]
    public class PlayerData : ISaveable
    {
        [field: SerializeField] public SerializableGuid Id { get; set; }

        // Transform 
        public Vector3 Position;
        public Vector3 RotationEuler;

        // Movement
        public Vector3 CurrentVelocity;

        // Gold
        public float OwnedGold = 0;

        // Stamina
        public float CurrentStamina = 0;
        public bool IsExhausted = false;
        public bool CanRegenStamina = true;
    }

    [RequireComponent(typeof(PlayerMovement), typeof(PlayerStamina), typeof(PlayerGold))]

    public class Player : MonoBehaviour, IBind<PlayerData>
    {
        [field: SerializeField] public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();

        private PlayerData _playerData;
        private PlayerMovement _movement;
        private PlayerGold _gold;
        private PlayerStamina _stamina;

        [SerializeField]
        private PlayerAnimation _animation;
        
        private void Awake()
        {
            _gold = GetComponent<PlayerGold>();
            _movement = GetComponent<PlayerMovement>();
            _stamina = GetComponent<PlayerStamina>();
        }

        public void Bind(PlayerData data)
        {
            _playerData = data;
            data.Id = Id;

            _movement.SetPlayerData(data);
            _gold.SetPlayerData(data);
            _stamina.SetPlayerData(data);
            _animation.SetPlayerData(data);

            transform.SetPositionAndRotation(_playerData.Position, Quaternion.Euler(_playerData.RotationEuler));
        }

        public void FixedUpdate()
        {
            _playerData.RotationEuler = transform.rotation.eulerAngles;
            _playerData.Position = transform.position;
        }

    }
}