using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// 플레이어의 상호작용을 담당
public class PlayerInteractor : MonoBehaviour
{
    private PlayerInput _playerInput;
    private IInteractable _nearestInteractableObject;
    private List<IInteractable> _interactableObjects = new List<IInteractable>();

    private void Awake()
    {
        _playerInput = GetComponentInParent<PlayerInput>();

        if (_playerInput != null)
        {
            _playerInput.actions["Interact"].performed += OnInteractContext;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}의 부모 객체에서 PlayerInput을 찾을 수 없습니다!");
        }
    }

    private void OnInteractContext(InputAction.CallbackContext context)
    {
        if (_nearestInteractableObject != null && _nearestInteractableObject.IsInteractable())
        {
            _nearestInteractableObject.Interact();
        }
    }

    private void OnDestroy()
    {
        _playerInput.actions["Interact"].performed -= OnInteractContext;
    }

    private void Update()
    {
        UpdateNearestObject();
    }

    private void UpdateNearestObject()
    {
        IInteractable closest = null;
        float minDistance = float.MaxValue;

        // 리스트를 돌며 가장 가까운 오브젝트 계산
        for (int i = _interactableObjects.Count - 1; i >= 0; i--)
        {
            var interactable = _interactableObjects[i];

            // 오브젝트가 파괴되었거나 비활성화된 경우 리스트에서 제거
            if (interactable == null || (interactable is MonoBehaviour mb && !mb.gameObject.activeInHierarchy))
            {
                _interactableObjects.RemoveAt(i);
                continue;
            }

            float distance = Vector3.Distance(transform.position, ((MonoBehaviour)interactable).transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = interactable;
            }
        }

        // 포커스가 변경되었을 때만 이벤트 호출 
        if (_nearestInteractableObject != closest)
        {
            _nearestInteractableObject?.OnExitFocus(); // 이전 대상 해제
            _nearestInteractableObject = closest;
            _nearestInteractableObject?.OnEnterFocus(); // 새 대상 강조
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // IInteractable 인터페이스를 가지고 있는지 확인
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            if (!_interactableObjects.Contains(interactable))
            {
                _interactableObjects.Add(interactable);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            // 리스트에서 제거될 때 포커스도 함께 해제
            if (_nearestInteractableObject == interactable)
            {
                interactable.OnExitFocus();
                _nearestInteractableObject = null;
            }

            _interactableObjects.Remove(interactable);
        }
    }
}
