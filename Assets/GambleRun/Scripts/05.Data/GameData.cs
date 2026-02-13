using System;
using System.Collections.Generic;
using GambleRun.Player;

namespace GambleRun.Core
{
    [Serializable]
    public class GameData
    {
        public string FileName = "Save";
        public string CurrentLevelName = "S";

        public PlayerData Player;
        public List<StorageData> Storages;

        public GamePersistanceData PersistanceData;
        public GameSessionData SessionData;
    }

    /// <summary>
    /// 영구적으로 유지되는 데이터
    /// ex)창고, 퀘스트, 플레이어 레벨, 플레이어 소지품등 
    /// </summary>
    [Serializable]
    public class GamePersistanceData
    {
        public List<StorageData> Storages; // 창고, 장비창, 가방 등등


    }

    public enum SessionState
    {
        New, 
        Start,
        End,
    }

    /// <summary>
    /// 게임 세션에 데이터
    /// ex) 전리품상자 아이템, 플레이어 위치와 상태, 몬스터 상태
    /// </summary>
    [Serializable]
    public class GameSessionData
    {
        public string SceneName; // 씬의 이름
        public SessionState State = SessionState.New;

        public PlayerData Player;
        public List <StorageData> Storages;
       
        // TODO : 전리품, 몬스터 등등
    }


}