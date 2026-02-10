using GambleRun.Events;
using UnityEngine;


namespace GambleRun
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
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private OnGoldChangedEvent _onGoldChangedEvent;

        public float _ownedGold = 100;
        
        // Update is called once per frame
        void Update()
        {
            AddGold(Time.deltaTime * 100f);
        }

        public void AddGold(float gold)
        {
            float prev = _ownedGold;
            _ownedGold += gold;

            _onGoldChangedEvent.Raise(new GoldChangedContext(prev, _ownedGold));
        }
    }

}