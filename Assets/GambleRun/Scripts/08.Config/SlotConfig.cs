using GambleRun.Items;
using UnityEngine;


namespace GambleRun.Config
{
    [CreateAssetMenu(fileName = "SlotConfig", menuName = "Config/SlotConfig")]
    public class SlotConfig : ScriptableObject
    {
        [SerializeField] private Sprite _searchIcon;
        public Sprite SearchIcon => _searchIcon;

        [SerializeField] private Color _commonColor = new Color(245 / 255f, 245 / 255f, 245 / 255f);    // #F5F5F5
        [SerializeField] private Color _uncommonColor = new Color(76 / 255f, 175 / 255f, 80 / 255f);   // #4CAF50
        [SerializeField] private Color _rareColor = new Color(33 / 255f, 150 / 255f, 243 / 255f);       // #2196F3
        [SerializeField] private Color _epicColor = new Color(156 / 255f, 39 / 255f, 176 / 255f);       // #9C27B0
        [SerializeField] private Color _legendaryColor = new Color(255 / 255f, 215 / 255f, 0 / 255f);   // #

        public Color GetRarityColor(ItemRarity rarity)
        {
            return rarity switch
            {
                ItemRarity.Common => _commonColor,
                ItemRarity.Uncommon => _uncommonColor,
                ItemRarity.Rare => _rareColor,
                ItemRarity.Epic => _epicColor,
                ItemRarity.Legendary => _legendaryColor,
                _ => Color.white
            };
        }
    }

}