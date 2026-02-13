using UnityEngine;
using UnityEngine.UIElements;


namespace GambleRun.UI
{
    /// <summary>
    /// 로딩화면 View를 담당
    /// </summary>
    public class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        ProgressBar _progressBar;

        void Start()
        {
            if (_uiDocument != null)
            {
                _progressBar = _uiDocument.rootVisualElement.Q<ProgressBar>();
            }
        }

        public void SetLoadingRatio(float ratio)
        {
            _progressBar.value = ratio;
        }

        public void CloseLoadingScreen()
        {
            Destroy(gameObject);
        }
    }
}