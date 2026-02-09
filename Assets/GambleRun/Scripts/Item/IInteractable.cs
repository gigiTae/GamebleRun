using UnityEngine;


namespace GambleRun
{
    // 상호작용 인터페이스 
    public interface IInteractable
    {
        void Interact(); // 상호작용 실행
        bool IsInteractable(); // 현재 상호작용 가능한 상태인지 확인
        void OnEnterFocus();
        void OnExitFocus();
    }
}