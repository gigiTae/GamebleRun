using UnityEditor;
using UnityEngine;

namespace GambleRun.Items
{
    // 아이템 등급
    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    public enum ItemType
    {
        Backpack,      // 가방
        Accessory,     // 장신구
        Consumable,    // 소비 아이템
        Material,      // 제작 재료
        Etc            // 잡탬 
    }

    [CreateAssetMenu(fileName = "ItemData", menuName = "GameData/ItemData")]
    public class ItemData : ScriptableObject
    {
        public SerializableGuid Id = SerializableGuid.NewGuid();

        [Header("Base")]
        public string ItemName;
        public int Price = 1;
        public float Weight = 0;
        public string Descrition;

        [Header("Stacking")]
        public uint MaxStack = 99;      // 최대 중첩 수

        public bool IsStackable => MaxStack > 1;

        [Header("Visual & Type")]
        public Sprite Icon;
        public ItemRarity Rarity = ItemRarity.Common;
        public ItemType Type = ItemType.Etc;

        public Item Create(uint quantity)
        {
            return new Item(this, quantity);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ItemData))] // ItemData 클래스를 위한 커스텀 에디터임을 명시
    public class ItemDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // 기본 인스펙터 속성들을 먼저 출력
            DrawDefaultInspector();

            ItemData data = (ItemData)target;

            // 아이콘이 있을 경우에만 미리보기 출력
            if (data.Icon != null)
            {
                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField("Icon Preview", EditorStyles.boldLabel);

                // 이미지의 텍스처를 가져와서 박스 안에 출력
                Rect rect = GUILayoutUtility.GetRect(100, 100); // 미리보기 크기 설정
                GUI.DrawTexture(rect, data.Icon.texture, ScaleMode.ScaleToFit);
            }
        }
    }
#endif
}
