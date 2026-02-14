using GambleRun;
using UnityEngine;
using GambleRun.Event;


namespace GambleRun.Props
{
    /// <summary>
    /// 탈출 지점
    /// </summary>
    public class ExitPoint : MonoBehaviour, IInteractable
    {
        [SerializeField] private RequestEndSessionEvent _endSessionEvent;

        public void Interact()
        {
            Debug.Log("EndSession");
            _endSessionEvent.Raise();
        }

        public bool IsInteractable()
        {
            return true;
        }
        public void OnEnterFocus()
        {

        }

        public void OnExitFocus()
        {

        }
    }

}
