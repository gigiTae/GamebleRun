using UnityEngine;

using GambleRun.UI;

namespace GambleRun.Event
{
    [CreateAssetMenu(fileName = "OnProgressBarUpdate", menuName = "Events/OnProgressBarUpdate")]
    public class OnProgressBarUpdate : EventSO<ProgressBarContext> { }
}