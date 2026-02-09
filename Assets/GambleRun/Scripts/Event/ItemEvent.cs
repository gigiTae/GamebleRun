using UnityEngine;



namespace GambleRun
{
    // 아이템 박스 이벤트 
    [CreateAssetMenu(fileName = "LootEvent", menuName = "Events/LootEvent")]
    public class LootEvent : EventSO<StorageData> { }
}