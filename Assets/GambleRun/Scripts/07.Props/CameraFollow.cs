using UnityEngine;


namespace GambleRun.Props
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private Transform _target;
        public void SetTarget(Transform target)
        {
            _target = target;
            FollowTarget(false);
        }

        void LateUpdate() // 이동이 끝난 후 카메라가 따라가도록 LateUpdate 사용
        {
            FollowTarget();
        }

        private void FollowTarget(bool isSmooth = true)
        {
            if (_target != null)
            {
                Vector3 desiredPosition = _target.position + _offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
                transform.position = isSmooth ? smoothedPosition : desiredPosition;
            }
        }
    }
}