using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Skill_System
{
    public class NearestVampireDamageEffect : MonoBehaviour
    {
        [SerializeField] private AuraSingleDetector _detector;
        [SerializeField, Min(0f)] private float _damagePerSecond;
        [SerializeField, Range(0, 1)] private float _healingPercentage;

        private readonly WaitForSeconds _instant = new(0f);

        public IEnumerator Run(ExecutorData executorData)
        {
            float dealtDamage;

            foreach (IDamageable unit in _detector.FoundTargets(executorData))
            {
                if (unit != null)
                {
                    dealtDamage = unit.TakeDamage(_damagePerSecond * Time.deltaTime);
                    executorData.ExecutorReplenishable.Replenish(dealtDamage * _healingPercentage);
                }
            }

            yield return _instant;
        }
    }
}