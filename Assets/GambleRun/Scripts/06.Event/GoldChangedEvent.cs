using UnityEngine;

using GambleRun.Player;

namespace GambleRun.Event
{
    [CreateAssetMenu(fileName = "OnGoldChangedEvent", menuName = "Events/GoldChangedEvent")]
    public class OnGoldChangedEvent : EventSO<GoldChangedContext> { }
}