using UnityEngine;

using GambleRun.Input;

namespace GambleRun.Player
{
    public enum MovementState
    {
        Walking,
    }

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerSettingData _settingData;
        private CharacterController _controller;
        private Camera _mainCamera;
        private PlayerStamina _stamina;

        private PlayerData _data;
        public void SetPlayerData(PlayerData data)
        {
            _data = data;
        }

        [Header("Input")]
        [SerializeField] private InputManager _inputManager;
        private DefaultInputAction.GamePlayActions _gamePlayActions;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _stamina = GetComponent<PlayerStamina>();

            if (_inputManager != null)
            {
                _gamePlayActions = _inputManager.GamePlayActions;
            }
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        void Update()
        {
            ApplyInput();
        }

        private void ApplyInput()
        {
            // 1. 입력값 읽기
            Vector2 moveInput = _gamePlayActions.Move.ReadValue<Vector2>();
            Vector3 moveDirection = Vector3.zero;

            // 2. 입력이 있을 때만 방향 계산 및 회전
            if (moveInput.sqrMagnitude > 0.01f)
            {
                Vector3 right = _mainCamera.transform.right;
                Vector3 forward = _mainCamera.transform.forward;
                forward.y = 0;
                right.y = 0;
                forward.Normalize();
                right.Normalize();

                moveDirection = (right * moveInput.x + forward * moveInput.y).normalized;

                // 회전 처리
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _settingData.RotationSpeed * Time.deltaTime);
            }

            // 3. 속도 결정 (입력이 없으면 자동으로 0)
            bool canSprint = !_data.IsExhausted && _gamePlayActions.Sprint.IsPressed();
            float currentSpeed = canSprint ? _settingData.SprintSpeed : _settingData.MoveSpeed;

            _data.CurrentVelocity.x = moveDirection.x * currentSpeed;
            _data.CurrentVelocity.z = moveDirection.z * currentSpeed;

            // 4. 중력은 입력 여부와 상관없이 항상 적용
            ApplyGravity();

            // 5. 최종 이동
            _controller.Move(_data.CurrentVelocity * Time.deltaTime);

            // 6. 스태미나 소모 (실제로 움직이고 있고 달리기 중일 때만)
            if (canSprint && moveInput.sqrMagnitude > 0.01f)
            {
                // TODO : 의존성 제거 할까?
                _stamina.UseStamina(Time.deltaTime * _settingData.SprintStaminaCost);
            }
        }

        private void ApplyGravity()
        {
            if (_controller.isGrounded)
            {
                _data.CurrentVelocity.y = 0f;
            }

            _data.CurrentVelocity.y += _settingData.Gravity * Time.deltaTime;
        }
    }
}