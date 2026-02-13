using UnityEngine;

namespace GambleRun.Player
{
    [CreateAssetMenu(fileName = "PlayerSettingData", menuName = "Player/PlayerSettingData")]
    public class PlayerSettingData : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 5.0f;
        [SerializeField] private float _sprintSpeed = 10.0f;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private float _gravity = 10f;

        public float MoveSpeed => _moveSpeed;
        public float SprintSpeed => _sprintSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float Gravity => _gravity;

        [Header("Stamina")]
        [SerializeField] private float _maxStamina = 100f;
        [SerializeField] private float _staminaRegenRate = 10f;
        [SerializeField] private float _sprintStaminaCost = 10f;
        [SerializeField] private float _regenDelayTime = 0.5f;
        [SerializeField] private float _exhaustedRecoverThreshold = 30f;

        public float ExhaustedRecoverThreshold => _exhaustedRecoverThreshold;
        public float RegenDelayTime => _regenDelayTime;
        public float MaxStamina => _maxStamina;
        public float StaminaRegenRate => _staminaRegenRate;
        public float SprintStaminaCost => _sprintStaminaCost;
    }
}