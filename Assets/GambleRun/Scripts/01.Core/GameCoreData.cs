using GambleRun.Config;
using UnityEngine;


namespace GambleRun.Core
{
    /// <summary>
    ///
    /// </summary>
    [CreateAssetMenu(fileName = "GameCoreData", menuName = "Core/GameCoreData")]
    public class GameCoreData : ScriptableObject
    {
        [Header("SceneName")]
        public string InGameScene;
        public string ReadyScene;

        [Header("Serialize")]
        public string SaveFileName;

        [Header("Config")]
        public DefaultGameConfig GameeConfig;
        // 1. 플레이어 기본 창고 정보


        // 2. 무료 로드아웃 아이템 정보
        


    }

}