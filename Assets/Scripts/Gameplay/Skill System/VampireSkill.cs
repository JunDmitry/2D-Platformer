using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Skill_System
{
    public class VampireSkill : MonoBehaviour
    {
        [SerializeField] private NearestVampireDamageEffect _effect;
        [SerializeField, Min(0f)] private float _durationInSeconds;
        [SerializeField, Min(0f)] private float _cooldownInSeconds;

        private HashSet<int> _executed = new();
        private WaitForSeconds _waitingCooldown;

        public event Action<ExecutorData> FailedExecute;

        public event Action<float> StartedDuration;

        public event Action<float, float> TickedDuration;

        public event Action<float> StartedCooldown;

        public event Action EndedCooldown;

        private void Awake()
        {
            _waitingCooldown = new(_cooldownInSeconds);
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
            StartedDuration?.Invoke(_durationInSeconds);

            yield return BeginDuration(executorData, _durationInSeconds);

            StartedCooldown?.Invoke(_cooldownInSeconds);
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

                yield return _effect.Run(executorData);

                TickedDuration?.Invoke(elapsedSeconds, duration);
                yield return null;
            }
        }
    }
}