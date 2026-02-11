using UnityEngine;

namespace GambleRun.Events
{
    [CreateAssetMenu(fileName = "OnGoldChangedEvent", menuName = "Events/GoldChangedEvent")]
    public class OnGoldChangedEvent : EventSO<GoldChangedContext> { }
}