using System;
using System.Collections.Generic;
using GambleRun.Player;
using GambleRun.Storages;

namespace GambleRun.Persistence
{
    [Serializable]
    public class GameData
    {
        public GamePersistanceData PersistanceData = new();
        public GameSessionData SessionData = new();
    }

    /// <summary>
    /// 영구적으로 유지되는 데이터
    /// ex)창고, 퀘스트, 플레이어 레벨, 플레이어 소지품등 
    /// </summary>
    [Serializable]
    public class GamePersistanceData
    {
        public List<StorageData> Storages = new(); // 창고, 장비창, 가방 등등
    }

    /// <summary>
    /// 게임 세션에 데이터
    /// ex) 전리품상자 아이템, 플레이어 위치와 상태, 몬스터 상태
    public enum SessionState
    {
        New,    // 새로운 게임시작
        Load,   // 기존 게임 로드
        Run,    // 게임 진행중
        End,    // 게임 종료
    }

    /// </summary>
    [Serializable]
    public class GameSessionData
    {
        public string SceneName; // 씬의 이름
        public SessionState State = SessionState.New;

        public PlayerData Player = new();
        public List <StorageData> Storages = new();

        // TODO : 전리품, 몬스터 등등
    }


}