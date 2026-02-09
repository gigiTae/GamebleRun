using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target; 
    [SerializeField] private Vector3 _offset;   
    [SerializeField] private float _smoothSpeed = 0.125f;

    void LateUpdate() // 이동이 끝난 후 카메라가 따라가도록 LateUpdate 사용
    {
        // 목표 위치 = 플레이어 위치 + 고정된 오프셋
        Vector3 desiredPosition = _target.position + _offset;

        // 부드럽게 따라가기 (선택 사항)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);

        transform.position = smoothedPosition;

        // 카메라는 항상 미리 설정된 각도(쿼터뷰)를 유지하며 회전하지 않음
    }
}
