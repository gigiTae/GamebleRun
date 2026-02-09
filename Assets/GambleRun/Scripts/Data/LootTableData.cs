using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;



namespace GambleRun
{
    [System.Serializable]
    public struct ProbabilityItem
    {
        public ItemData Item;     // 아이템 데이터
        public float Probability; // 확률
    }


    [CreateAssetMenu(fileName = "LootTableData", menuName = "GameData/LootTableData")]
    public class LootTableData : ScriptableObject
    {
        public int SlotCount = 1; // 전리품 슬롯

        // TODO : MinMaxSlotCount에 따른 확률추가

        public List<ItemData> FixedItems; // 고정 아이템
        public List<ProbabilityItem> ProbabilityItems;

        public ItemData GetRandomCloneItem()
        {
            if (ProbabilityItems == null || ProbabilityItems.Count == 0) return null;

            // 1. 전체 확률의 총합 계산
            float totalProbability = 0;
            foreach (var item in ProbabilityItems)
            {
                totalProbability += item.Probability;
            }

            // 2. 0 ~ 총합 사이의 랜덤 값 결정
            float randomPoint = Random.value * totalProbability;

            // 3. 랜덤 값이 어느 아이템 구간에 속하는지 확인
            float currentSum = 0;
            foreach (var item in ProbabilityItems)
            {
                currentSum += item.Probability;
                if (randomPoint <= currentSum)
                {
                    return item.Item.Clone();
                }
            }

            return null;
        }
    }

}