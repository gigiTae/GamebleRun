using UnityEngine;
using UnityEngine.UIElements;

namespace GambleRun.UI
{
    public class ReadyScreenView : MonoBehaviour
    {
        private UIDocument _uiDocument;

        private Button _startButton;
        private Button _exitButton;


        private void OnEnable()
        {
            _uiDocument = GetComponent<UIDocument>();

            if (_uiDocument != null)
            {
                _startButton = _uiDocument.rootVisualElement.Q<Button>("start_button");
                _exitButton = _uiDocument.rootVisualElement.Q<Button>("exit_button");
                _startButton.clicked += StartGame;
                _exitButton.clicked += ExitGame;
            }
        }

        private void OnDisable()
        {
            _startButton.clicked -= StartGame;
            _exitButton.clicked -= ExitGame;
        }


        private void StartGame()
        {
            Debug.Log("Start");
        }

        private void ExitGame()
        {
            Debug.Log("End");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}