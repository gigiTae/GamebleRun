using GambleRun.Events;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

namespace GambleRun
{
    public class GoldView : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private string _goldLabelName;
        [SerializeField] private OnGoldChangedEvent _onGoldChangedEvent;

        private Label _goldLabel;

        private void Awake()
        {
            if (_uiDocument != null)
            {
                _goldLabel = _uiDocument.rootVisualElement.Q<Label>(_goldLabelName);
            }

            if (_onGoldChangedEvent != null)
            {
                _onGoldChangedEvent.Subscribe(OnGoldChanged);
            }
        }

        private void OnDestroy()
        {
            if (_onGoldChangedEvent != null)
            {
                _onGoldChangedEvent.Unsubscribe(OnGoldChanged);
            }
        }

        private void OnGoldChanged(GoldChangedContext context)
        {
            int gold = Mathf.RoundToInt(context.Current);
            _goldLabel.text = gold.ToString();
        }
    }

}