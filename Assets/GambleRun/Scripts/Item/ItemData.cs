using UnityEngine;


// 아이템 등급
public enum ItemRarity
{
    Common,  
    Uncommon,
    Rare, 
    Epic,
    Legendary
}

// 아이템 기본적인 데이터를 설정
[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string ItemName = "Empty";
    public int Price = 1;
    public int Weight = 0;  
    public uint Count = 1;
    public Sprite Icon = null;
    public ItemRarity Rarity = ItemRarity.Common;

} 
