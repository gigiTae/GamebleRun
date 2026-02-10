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
        [SerializeField] private PlayerData _playerData;

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
            _currentStamina = _playerData.MaxStamina;

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
            if (_isExhausted && _currentStamina >= _playerData.ExhaustedRecoverThreshold)
            {
                _isExhausted = false;
            }
        }

        private void RegenStamina()
        {
            if (_canRegenStamina)
            {
                float regenStamina = Time.deltaTime * _playerData.StaminaRegenRate;
                _currentStamina = Mathf.Min(_currentStamina + regenStamina, _playerData.MaxStamina);
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
            yield return new WaitForSeconds(_playerData.RegenDelayTime);
            _canRegenStamina = true;
        }

        private void UpdateView()
        {
            float ratio = _currentStamina / _playerData.MaxStamina;
            _staminaChangedEvent.Raise(new ProgressBarContext(ratio));
        }

    }
}