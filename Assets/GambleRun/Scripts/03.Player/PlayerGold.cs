using GambleRun.Event;
using UnityEngine;


namespace GambleRun.Player
{
    public readonly struct GoldChangedContext
    {
        public readonly float Prev;
        public readonly float Current;

        public GoldChangedContext(float prev, float current)
        {
            Prev = prev;
            Current = current;
        }
    }

    /// <summary>
    /// 인게임 내에서 재화와 Hp역할을 하는 클래스
    /// </summary>
    public class PlayerGold : MonoBehaviour
    {
        [SerializeField] private PlayerSettingData _playerSettingData;
        [SerializeField] private OnGoldChangedEvent _onGoldChangedEvent;

        private PlayerData _data;
        public void SetPlayerData(PlayerData playerData)
        {
            _data = playerData;
        }

        void Update()
        {
            AddGold(Time.deltaTime * 100f);
        }

        public void AddGold(float gold)
        {
            float prev = _data.OwnedGold;
            _data.OwnedGold += gold;

            _onGoldChangedEvent.Raise(new GoldChangedContext(prev, _data.OwnedGold));
        }
    }

}