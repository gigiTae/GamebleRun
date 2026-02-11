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

        private float _currentStamina;
        private bool _isExhausted;
        private bool _canRegenStamina;

        private Coroutine _regenDelayCorutine;

        public bool IsExhausted => _isExhausted;

        private void Awake()
        {
        }

        private void Start()
        {
            _isExhausted = false;
            _currentStamina = _playerSettingData.MaxStamina;

            UpdateView();
            _canRegenStamina = true;
        }

        void Update()
        {
            RegenStamina();
            CheckExhausted();
        }

        private void CheckExhausted()
        {
            if (_isExhausted && _currentStamina >= _playerSettingData.ExhaustedRecoverThreshold)
            {
                _isExhausted = false;
            }
        }

        private void RegenStamina()
        {
            if (_canRegenStamina)
            {
                float regenStamina = Time.deltaTime * _playerSettingData.StaminaRegenRate;
                _currentStamina = Mathf.Min(_currentStamina + regenStamina, _playerSettingData.MaxStamina);
                UpdateView();
            }
        }

        public void UseStamina(float amount)
        {
            _currentStamina = Mathf.Max(0f, _currentStamina - amount);

            if (_currentStamina <= 0f)
            {
                _isExhausted = true;
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
            _canRegenStamina = false;
            yield return new WaitForSeconds(_playerSettingData.RegenDelayTime);
            _canRegenStamina = true;
        }

        private void UpdateView()
        {
            float ratio = _currentStamina / _playerSettingData.MaxStamina;
            _staminaChangedEvent.Raise(new ProgressBarContext(ratio));
        }

    }
}