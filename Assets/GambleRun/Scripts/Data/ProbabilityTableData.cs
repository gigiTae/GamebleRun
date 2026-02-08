using GambleRun;
using UnityEngine;
using System.Collections.Generic;

namespace GambleRun
{
    [System.Serializable]
    public struct ProbabilityItem
    {
        public ItemData Item;     // 아이템 데이터
        public float Probability; // 확률
    }


    // 아이템-확률 분포를 담은 데이터
    [CreateAssetMenu(fileName = "ProbabilityTable", menuName = "GameData/ProbabilityTable")]
    public class ProbabilityTable : ScriptableObject
    {
        public List<ProbabilityItem> ProbabilityItems;
    }

}