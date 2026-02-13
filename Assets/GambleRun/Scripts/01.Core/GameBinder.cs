using GambleRun.Persistence;
using GambleRun.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;


namespace GambleRun.Core
{
    /// <summary>
    /// 게임 데이터의 바인딩을 담당
    /// </summary>
    public class GameDataBinder : MonoBehaviour
    {
        public void BindGameData(GameMode mode, GameData data)
        {
            if (mode == GameMode.Ready)
            {
                BindReadyMode(data);
            }
            else if (mode == GameMode.InGame)
            {
                BindInGameMode(data);
            }
        }

        private void BindReadyMode(GameData data)
        {


        }

        /// <summary>
        /// InGame Data 적용
        /// </summary>
        private void BindInGameMode(GameData data)
        {
            GameSessionData sessionData = data.SessionData;

            if (sessionData.State == SessionState.New)
            {
                // 게임 시작 정보 가져오기

                // 1. PlayerStorage


            }
            else if (sessionData.State == SessionState.Load)
            {

            }
            else
            {
                Debug.LogWarning("Session State Assert");
            }


            // Bind 
            sessionData.Player = Bind<PlayerBinder, PlayerData>(sessionData.Player);
            sessionData.Storages = Bind<StorageDataBinder, StorageData>(sessionData.Storages);
            
            // State
            sessionData.State = SessionState.Run;
        }




        TData Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
        {
            var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
            if (entity != null)
            {
                // ** 전달값이 null이면 참조가 아니기때문에 원본객체를 수정할 수 없기때문에
                // ** new() 할당 받은 객체에 리턴값을 통해서 원복객체에 전달합니다
                // ** ref 방법도 있음
                if (data == null)
                {
                    data = new TData { Id = entity.Id };
                }
                entity.Bind(data);
            }

            return data;
        }
        List<TData> Bind<T, TData>(List<TData> datas) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
        {
            var entities = FindObjectsByType<T>(FindObjectsSortMode.None);

            foreach (var entity in entities)
            {
                if (datas == null)
                {
                    datas = new List<TData>();
                }

                var data = datas.FirstOrDefault(d => d.Id == entity.Id);
                if (data == null)
                {
                    data = new TData { Id = entity.Id };
                    datas.Add(data);
                }
                entity.Bind(data);
            }

            return datas;
        }
    }
}