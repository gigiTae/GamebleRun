using System.Collections.Generic;
using UnityEngine;


namespace GambleRun
{
    [System.Serializable]
    public struct ProbabilityItem
    {
        public ItemData Item;     // 아이템 데이터

        [UnityEngine.Range(0f, 100f)]
        public float Probability; // 당첨 확률
    }

    [CreateAssetMenu(fileName = "LootTableData", menuName = "GameData/LootTableData")]
    public class LootTableData : ScriptableObject
    {
        public int SlotCount = 1; // 전리품 슬롯

        public List<ProbabilityItem> ProbabilityItems;

        public List<ItemData> GetRandomCloneItems()
        {
            List<ItemData> items = new List<ItemData>(SlotCount);

            if (ProbabilityItems == null || ProbabilityItems.Count == 0)
            {
                FillEmptySlots(items);
                return items;
            }

            // 1. 원본 데이터를 보호하기 위해 복사본 생성
            List<ProbabilityItem> pool = new List<ProbabilityItem>(ProbabilityItems);
            
            // 2. Fisher-Yates Shuffle
            for (int i = pool.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                var temp = pool[i];
                pool[i] = pool[randomIndex];
                pool[randomIndex] = temp;
            }

            // 3. 당첨 확인 및 리스트 추가
            foreach (var data in pool)
            {
                if (items.Count >= SlotCount) break;

                float randomValue = Random.Range(0f, 100f);
                if (randomValue <= data.Probability)
                {
                    if (data.Item != null)
                        items.Add(data.Item.Clone());
                }   
            }

            FillEmptySlots(items);

            return items;
        }

        private void FillEmptySlots(List<ItemData> items)
        {
            while (items.Count < SlotCount)
            {
                items.Add(null);
            }
        }
    }
}