using Assets.Scripts.Gameplay.Skill_System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class VampireView : MonoBehaviour
    {
        [SerializeField] private VampireSkill _vampireSkill;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _imageForAura;

        [SerializeField] private Color _durationColor;
        [SerializeField] private Color _cooldownColor;

        private Color _originalColor;
        private Coroutine _cooldownCoroutine;

        private void Awake()
        {
            _vampireSkill.FailedExecute += OnFailedExecute;
            _vampireSkill.StartedDuration += OnStartedDuration;
            _vampireSkill.TickedDuration += OnTickedDuration;
            _vampireSkill.StartedCooldown += OnStartedCooldown;
            _vampireSkill.EndedCooldown += OnEndedCooldown;

            _originalColor = _slider.image.color;
            _imageForAura.enabled = false;
            _slider.maxValue = 1f;
            _slider.minValue = 0f;
        }

        private void OnFailedExecute(ExecutorData executor)
        {
        }

        private void OnStartedDuration(float duration)
        {
            _slider.image.color = _durationColor;
            _slider.value = _slider.maxValue;
            _imageForAura.enabled = true;
        }

        private void OnTickedDuration(float elapsedSeconds, float durationInSeconds)
        {
            _slider.value = _slider.maxValue - elapsedSeconds / durationInSeconds;
        }

        private void OnStartedCooldown(float cooldownInSeconds)
        {
            _imageForAura.enabled = false;
            _slider.image.color = _cooldownColor;
            _slider.value = 0f;

            _cooldownCoroutine = StartCoroutine(ChangeSliderValueSmoothly(cooldownInSeconds));
        }

        private void OnEndedCooldown()
        {
            if (_cooldownCoroutine != null)
            {
                StopCoroutine(_cooldownCoroutine);
                _cooldownCoroutine = null;
            }

            _slider.image.color = _originalColor;
            _slider.value = 1f;
        }

        private IEnumerator ChangeSliderValueSmoothly(float cooldownInSeconds)
        {
            float elapsedSeconds = 0f;
            float sourceValue = _slider.value;
            float targetValue = 1f;

            while (elapsedSeconds < cooldownInSeconds)
            {
                elapsedSeconds += Time.deltaTime;

                _slider.value = Mathf.Lerp(sourceValue, targetValue, elapsedSeconds / cooldownInSeconds);

                yield return null;
            }
        }
    }
}