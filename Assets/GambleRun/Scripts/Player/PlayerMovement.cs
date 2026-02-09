using GambleRun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _currentVelocity;
    private Camera _mainCamera;

    [SerializeField] private InputManager _inputManager;
    private DefaultInputAction.GamePlayActions _gamePlayActions;

    [SerializeField] private float _rotationSpeed = 10f; // 회전 속도 설정
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _gravity = -9.81f;

    public float HorizontalSpeedRatio
    {
        get
        {
            Vector3 horizontalVelocity = new Vector3(_currentVelocity.x, 0, _currentVelocity.z);
            return horizontalVelocity.magnitude / _moveSpeed;
        }
    }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _mainCamera = Camera.main;

        if (_inputManager != null)
        {
            _gamePlayActions = _inputManager.GamePlayActions;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        ApplyInput();
    }

    private void ApplyInput()
    {
        Vector2 moveInput = _gamePlayActions.Move.ReadValue<Vector2>();
        Vector3 right = _mainCamera.transform.right;
        Vector3 forward = _mainCamera.transform.forward;

        forward.y = 0; // 카메라가 아래를 보고 있어도 앞방향은 수평이 되도록 함
        right.y = 0;   // 우측 방향도 수평이 되도록 함 //  필요할까?

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = right * moveInput.x + forward * moveInput.y;
        Vector3 moveValue = moveDirection * _moveSpeed;

        // 플레이어 방향 설정
        if (moveInput.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        _currentVelocity.x = moveValue.x;
        _currentVelocity.z = moveValue.z;

        ApplyGravity();
        _controller.Move(_currentVelocity * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded)
        {
            _currentVelocity.y = 0f;
        }

        _currentVelocity.y += _gravity * Time.deltaTime;
    }
}
