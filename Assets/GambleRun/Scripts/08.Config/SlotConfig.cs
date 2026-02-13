using UnityEngine;


namespace GambleRun.Config
{
    [CreateAssetMenu(fileName = "SlotConfig", menuName = "Config/SlotConfig")]
    public class SlotConfig : ScriptableObject
    {
        [SerializeField] private Sprite _searchIcon;
        public Sprite SearchIcon => _searchIcon;
    }

}