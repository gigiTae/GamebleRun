using UnityEngine;

namespace GambleRun.Core
{
    /// <summary>
    /// 게임 초기화 데이터
    /// </summary>
    [CreateAssetMenu(fileName = "GameInit", menuName = "Core/GameInit")]
    public class GameInitData : ScriptableObject
    {
        public GameMode Mode = GameMode.Ready;
        public string SaveFileName = "Save";

        [Header("Prefabs")]
        public GameObject LoadingScreen;
        public GameObject GameCore;
        public GameObject Player;
        public GameObject Level;
    }
}
