using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Skill_System
{
    public class Skill : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _effect;
        [SerializeField] private SkillConfig _config;

        private ISkillEffect _skillEffect;
        private HashSet<int> _executed = new();
        private WaitForSeconds _waitingCooldown;

        public event Action<ExecutorData> FailedExecute;

        public event Action<float> StartedDuration;

        public event Action<float, float> TickedDuration;

        public event Action<float> StartedCooldown;

        public event Action EndedCooldown;

        private void Awake()
        {
            _skillEffect = _effect as ISkillEffect;
            _waitingCooldown = new(_config.CooldownInSeconds);
        }

        private void OnValidate()
        {
            if (_effect != null && _effect is not ISkillEffect)
                Debug.LogError($"{nameof(_effect)} on {name} must implement {nameof(ISkillEffect)}.", this);
        }

        public void Execute(in ExecutorData executorData)
        {
            if (_executed.Contains(executorData.Id))
            {
                FailedExecute?.Invoke(executorData);
                return;
            }

            StartCoroutine(Run(executorData));
        }

        private IEnumerator Run(ExecutorData executorData)
        {
            _executed.Add(executorData.Id);
            StartedDuration?.Invoke(_config.DurationInSeconds);

            yield return BeginDuration(executorData, _config.DurationInSeconds);

            StartedCooldown?.Invoke(_config.CooldownInSeconds);
            yield return _waitingCooldown;
            EndedCooldown?.Invoke();

            _executed.Remove(executorData.Id);
        }

        private IEnumerator BeginDuration(ExecutorData executorData, float duration)
        {
            float elapsedSeconds = 0f;

            while (elapsedSeconds < duration)
            {
                elapsedSeconds += Time.deltaTime;

                yield return _skillEffect.Run(executorData);

                TickedDuration?.Invoke(elapsedSeconds, duration);
                yield return null;
            }
        }
    }
}
