using UnityEngine;
using UnityEngine.UIElements;
using GambleRun.Events;

namespace GambleRun
{
    public readonly struct ProgressBarContext
    {
        public readonly float CurrentRatio; // (current/ max)

        public ProgressBarContext(float ratio, bool isVisible = true)
        {
            CurrentRatio = ratio;
        }
    }

    public class ProgressBarView : MonoBehaviour
    {
        [SerializeField] private OnProgressBarUpdate _progressChangedEvent;
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private string _progressBarName;
        [SerializeField] private bool _useHideOption;
        [SerializeField][Range(0f, 1f)] private float _hideRatio;

        private ProgressBar _progressBar;

        private void Awake()
        {
            if (_uiDocument == null) return;

            _progressBar = _uiDocument.rootVisualElement.Q<ProgressBar>(_progressBarName);

            if (_progressBar == null)
            {
                Debug.LogError($"{_progressBarName}을(를) 찾을 수 없습니다! UXML의 이름을 확인하세요.");
            }

            if (_progressChangedEvent != null)
            {
                _progressChangedEvent.Subscribe(OnChangedStamina);
            }
        }

        private void OnDestroy()
        {
            if (_progressChangedEvent != null)
            {
                _progressChangedEvent.Unsubscribe(OnChangedStamina);
            }
        }

        private void OnChangedStamina(ProgressBarContext context)
        {
            UpdateView(context.CurrentRatio);
        }

        public void UpdateView(float ratio)
        {
            if (_progressBar == null) return;
            _progressBar.value = ratio * 100f;

            if(_useHideOption && _hideRatio <= ratio)
            {
                _progressBar.style.visibility = Visibility.Hidden;
            }
            else
            {
                _progressBar.style.visibility = Visibility.Visible;
            }
        }
    }
}