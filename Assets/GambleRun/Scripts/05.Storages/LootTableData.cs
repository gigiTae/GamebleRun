using System.Collections.Generic;
using UnityEngine;
using GambleRun.Items;

namespace GambleRun.Storages
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

        public StorageData GetRandomCloneItems()
        {
            StorageData storageData = new StorageData();

            if (ProbabilityItems == null || ProbabilityItems.Count == 0)
            {
                FillEmptySlots(storageData);
                return storageData;
            }

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
                if (storageData.Items.Count >= SlotCount) break;

                float randomValue = Random.Range(0f, 100f);
                if (randomValue <= data.Probability)
                {
                    if (data.Item != null)
                    {
                        Item item = new Item(data.Item, 1);
                        storageData.Items.Add(item);
                    }
                }
            }
            FillEmptySlots(storageData);

            return storageData;
        }

        private void FillEmptySlots(StorageData data)
        {
            while (data.Items.Count < SlotCount)
            {
                data.Items.Add(null);
            }
        }
    }
}