using GambleRun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GambleRun
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        private CharacterController _controller;
        private Vector3 _currentVelocity;
        private Camera _mainCamera;

        private PlayerStamina _playerStamina;

        [Header("Input")]
        [SerializeField] private InputManager _inputManager;
        private DefaultInputAction.GamePlayActions _gamePlayActions;

        public float HorizontalSpeedRatio
        {
            get
            {
                Vector3 horizontalVelocity = new Vector3(_currentVelocity.x, 0, _currentVelocity.z);
                return horizontalVelocity.magnitude / _playerData.SprintSpeed;
            }
        }

        private void Awake()
        {
            _playerStamina = GetComponent<PlayerStamina>();
            _controller = GetComponent<CharacterController>();
            _mainCamera = Camera.main;

            if (_inputManager != null)
            {
                _gamePlayActions = _inputManager.GamePlayActions;
            }
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
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _playerData.RotationSpeed * Time.deltaTime);
            }

            // 3. 속도 결정 (입력이 없으면 자동으로 0)
            bool canSprint = !_playerStamina.IsExhausted && _gamePlayActions.Sprint.IsPressed();
            float currentSpeed = canSprint ? _playerData.SprintSpeed : _playerData.MoveSpeed;

            _currentVelocity.x = moveDirection.x * currentSpeed;
            _currentVelocity.z = moveDirection.z * currentSpeed;

            // 4. 중력은 입력 여부와 상관없이 항상 적용
            ApplyGravity();

            // 5. 최종 이동
            _controller.Move(_currentVelocity * Time.deltaTime);

            // 6. 스태미나 소모 (실제로 움직이고 있고 달리기 중일 때만)
            if (canSprint && moveInput.sqrMagnitude > 0.01f)
            {
                _playerStamina.UseStamina(Time.deltaTime* _playerData.SprintStaminaCost ); // 예시: 초당 소모
            }
        }

        private void ApplyGravity()
        {
            if (_controller.isGrounded)
            {
                _currentVelocity.y = 0f;
            }

            _currentVelocity.y += _playerData.Gravity * Time.deltaTime;
        }
    }
}