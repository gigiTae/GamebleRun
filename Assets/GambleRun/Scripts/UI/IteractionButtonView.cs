using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

namespace GambleRun
{
    public class InteractionButtonView : MonoBehaviour
    {
        private VisualElement _interactionPanel;

        [SerializeField] private Vector3 _offset = new Vector3(0, 0, 0); // 박스 머리 위 높이 조절

        private void Awake()
        {
            _interactionPanel = GetComponent<UIDocument>().rootVisualElement;

            if (_interactionPanel != null)
            {
                Show(false);
            }
        }
        public void Show(bool visible)
        {
            _interactionPanel.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
        }

        void LateUpdate()
        {
            UpdateUIPosition();
        }

        private void UpdateUIPosition()
        {
            if (_interactionPanel.style.display == DisplayStyle.None) return;

            Vector3 position = GetComponent<Transform>().position;
            Vector2 screenPos = Camera.main.WorldToScreenPoint(position + _offset);

            float xPos = screenPos.x;
            float yPos = Screen.height - screenPos.y;

            _interactionPanel.style.left = xPos;
            _interactionPanel.style.top = yPos;
        }

    }
}
