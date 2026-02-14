using GambleRun.Persistence;
using GambleRun.Player;
using GambleRun.Storages;
using JetBrains.Annotations;
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
            GameSessionData sessionData = data.SessionData;
            GamePersistanceData persistanceData = data.PersistanceData;

            // 스토리지 정보 바인딩
            persistanceData.Storages = Bind<StorageDataBinder, StorageData>(persistanceData.Storages);
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



        [MustUseReturnValue("반환값은 원본객체에 다시 바인딩이 필요합니다")]
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

        [MustUseReturnValue("반환값은 원본객체에 다시 바인딩이 필요합니다")]
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