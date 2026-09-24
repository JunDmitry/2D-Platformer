using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Skill_System
{
    public class NearestVampireDamageEffect : MonoBehaviour, ISkillEffect
    {
        [SerializeField] private AuraSingleDetector _detector;
        [SerializeField, Min(0f)] private float _damagePerSecond;
        [SerializeField, Range(0, 1)] private float _healingPercentage;

        private readonly WaitForSeconds _instant = new(0f);

        public IEnumerator Run(ExecutorData executorData)
        {
            IDamageable target = _detector.FindNearestTarget(executorData);

            if (target != null)
            {
                float dealtDamage = target.TakeDamage(_damagePerSecond * Time.deltaTime);
                executorData.ExecutorReplenishable.Replenish(dealtDamage * _healingPercentage);
            }

            yield return _instant;
        }
    }
}
