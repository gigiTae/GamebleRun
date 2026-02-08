using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;



namespace GambleRun
{
    [CreateAssetMenu(fileName = "LootTableData", menuName = "GameData/LootTableData")]
    public class LootTableData : ScriptableObject
    {
        public int SlotCount = 1; // 전리품 슬롯

        // TODO : MinMaxSlotCount에 따른 확률추가

        public List<ItemData> FixedItems; // 고정 아이템
        public List<ProbabilityTable> ProbabilityItems; // 아이템 확률 분포
    }

}