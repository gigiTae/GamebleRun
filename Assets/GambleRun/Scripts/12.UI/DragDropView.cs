using GambleRun.Manager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


namespace GambleRun.UI
{
    /// <summary>
    /// 드래그 드랍 시 시각적 피드백(고스트 아이콘)을 담당합니다.
    /// </summary>
    public class DragDropView : MonoBehaviour
    {
        [SerializeField] private DragDropManager _manager;
        [SerializeField] private UIDocument _uiDocument;

        private VisualElement _root;
        private Image _ghostIcon;
        [SerializeField] private Vector2 _iconSize = new Vector2(100, 100); // 기획안의 슬롯 크기에 맞춰 조정 가능

        private void Awake()
        {
            _root = _uiDocument.rootVisualElement;

            _ghostIcon = new Image();
            _ghostIcon.style.position = Position.Absolute;

            _ghostIcon.style.width = _iconSize.x;
            _ghostIcon.style.height = _iconSize.y;

            _ghostIcon.style.backgroundColor = new Color(30f / 255f, 30f / 255f, 30f / 255f, 0.6f);

            // 테두리 설정 (border-width: 2px, border-color: rgb(74, 74, 74))
            _ghostIcon.style.borderLeftWidth = 2;
            _ghostIcon.style.borderRightWidth = 2;
            _ghostIcon.style.borderTopWidth = 2;
            _ghostIcon.style.borderBottomWidth = 2;
            _ghostIcon.style.borderLeftColor = new Color(74f / 255f, 74f / 255f, 74f / 255f);
            _ghostIcon.style.borderRightColor = new Color(74f / 255f, 74f / 255f, 74f / 255f);
            _ghostIcon.style.borderTopColor = new Color(74f / 255f, 74f / 255f, 74f / 255f);
            _ghostIcon.style.borderBottomColor = new Color(74f / 255f, 74f / 255f, 74f / 255f);

            // 모서리 둥글게 (border-radius: 10px)
            _ghostIcon.style.borderTopLeftRadius = 10;
            _ghostIcon.style.borderTopRightRadius = 10;
            _ghostIcon.style.borderBottomLeftRadius = 10;
            _ghostIcon.style.borderBottomRightRadius = 10;

            _ghostIcon.pickingMode = PickingMode.Ignore;

            SetVisible(false);
            _root.Add(_ghostIcon);
        }

        private void OnEnable()
        {
            _manager.OnDragStateChanged += OnDragStateChanged;
        }

        private void OnDisable()
        {
            _manager.OnDragStateChanged -= OnDragStateChanged;
        }

        private void OnDragStateChanged(bool isDragging)
        {
            if (isDragging)
            {
                _ghostIcon.sprite = _manager.GetDragIcon();
            }
            else
            {

            }

                SetVisible(isDragging);
        }

        private void SetVisible(bool isVisible)
        {
            _ghostIcon.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
        }

        void Update()
        {
            if (_manager.IsDragging)    
            {
                UpdateGhostPosition();
            }
        }

        /// <summary>
        /// 마우스 커서 위치(Screen Space)를 UI Toolkit 좌표계로 변환하여 아이콘 위치 업데이트
        /// </summary>
        private void UpdateGhostPosition()
        {
            // 1. 현재 마우스 위치 가져오기 (Screen Space: Bottom-Left 기준)
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            // 2. UI Toolkit의 루트 엘리먼트 기준 위치 가져오기 (월드 경계 좌표)
            // 일반적으로 0,0이지만, UI 패널이 특정 위치에 고정되어 있을 경우 오프셋을 계산해야 합니다.
            Vector2 rootPosition = _uiDocument.rootVisualElement.worldBound.position;

            // 3. 좌표 변환 로직 (유니티 하단 기준 -> UI Toolkit 상단 기준)
            // X축: 그대로 사용
            // Y축: 전체 화면 높이에서 마우스 Y값을 빼서 반전시킴 [1]
            float xPos = mousePosition.x - rootPosition.x;
            float yPos = Screen.height - mousePosition.y - rootPosition.y;

            // 4. 아이콘의 중심이 마우스 커서에 오도록 보정
            // 아이콘 크기(_iconSize)의 절반만큼 좌표에서 빼줍니다.
            float finalX = xPos - (_iconSize.x / 2f);
            float finalY = yPos - (_iconSize.y / 2f);

            // 5. 고스트 아이콘 스타일 업데이트
            _ghostIcon.style.left = finalX;
            _ghostIcon.style.top = finalY;


        }
    }
}