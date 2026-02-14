using UnityEngine;



namespace GambleRun.Config
{
    /// <summary>
    /// 게임 기본설정 값
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig")]
    public class DefaultGameConfig : ScriptableObject
    {
        [Header("Storage")]
        public int StoreCapacity;
        public int BackpackCapacity;
        public int EquipmentCapacity;

       

    }

}