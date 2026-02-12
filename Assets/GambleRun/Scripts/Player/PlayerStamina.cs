using GambleRun;
using GambleRun.Events;
using System.Collections;
using UnityEngine;


namespace GambleRun
{
    /// <summary>
    /// 플레이어의 스태미나를 관리하는 클래스
    /// </summary>
    public class PlayerStamina : MonoBehaviour
    {
        [SerializeField] private OnProgressBarUpdate _staminaChangedEvent;
        [SerializeField] private PlayerSettingData _playerSettingData;

        private PlayerData _data;
        public void SetPlayerData(PlayerData playerData)
        {
            _data = playerData;
        }

        private Coroutine _regenDelayCorutine;

        public bool IsExhausted => _data.IsExhausted;


        void Update()
        {
            RegenStamina();
            CheckExhausted();
        }

        private void CheckExhausted()
        {
            if (_data.IsExhausted && _data.CurrentStamina >= _playerSettingData.ExhaustedRecoverThreshold)
            {
                _data.IsExhausted = false;
            }
        }

        private void RegenStamina()
        {
            if (_data.CanRegenStamina)
            {
                float regenStamina = Time.deltaTime * _playerSettingData.StaminaRegenRate;
                _data.CurrentStamina = Mathf.Min(_data.CurrentStamina + regenStamina, _playerSettingData.MaxStamina);
                UpdateView();
            }
        }

        public void UseStamina(float amount)
        {
            _data.CurrentStamina = Mathf.Max(0f, _data.CurrentStamina - amount);

            if (_data.CurrentStamina <= 0f)
            {
                _data.IsExhausted = true;
            }

            UpdateView();

            if (_regenDelayCorutine != null)
            {
                StopCoroutine(_regenDelayCorutine);
            }

            _regenDelayCorutine = StartCoroutine(RegenDelayRoutine());
        }

        private IEnumerator RegenDelayRoutine()
        {
            _data.CanRegenStamina = false;
            yield return new WaitForSeconds(_playerSettingData.RegenDelayTime);
            _data.CanRegenStamina = true;
        }

        private void UpdateView()
        {
            float ratio = _data.CurrentStamina / _playerSettingData.MaxStamina;
            _staminaChangedEvent.Raise(new ProgressBarContext(ratio));
        }

    }
}