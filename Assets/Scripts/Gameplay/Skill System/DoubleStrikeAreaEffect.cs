using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Skill_System
{
    public class DoubleStrikeAreaEffect : MonoBehaviour, ISkillEffect
    {
        [SerializeField] private AuraAreaDetector _detector;
        [SerializeField, Min(0f)] private float _damage;
        [SerializeField, Min(0f)] private float _intervalInSeconds;

        public IEnumerator Run(ExecutorData executorData)
        {
            StrikeAll(executorData);

            yield return new WaitForSeconds(_intervalInSeconds);

            StrikeAll(executorData);
        }

        private void StrikeAll(ExecutorData executorData)
        {
            List<IDamageable> targets = _detector.FindTargetsInRadius(executorData);

            foreach (IDamageable target in targets)
                target.TakeDamage(_damage);
        }
    }
}
