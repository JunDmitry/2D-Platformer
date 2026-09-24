using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Skill_System
{
    public class KnockbackAreaEffect : MonoBehaviour, ISkillEffect
    {
        [SerializeField] private AuraAreaDetector _detector;
        [SerializeField, Min(0f)] private float _maxRangeFromCaster;
        [SerializeField, Min(0f)] private float _knockbackSpeedPerSecond;
        [SerializeField, Min(0f)] private float _knockbackDurationInSeconds;

        private readonly WaitForSeconds _instant = new(0f);

        public IEnumerator Run(ExecutorData executorData)
        {
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 offset = Vector2.ClampMagnitude(mouseWorldPoint - executorData.Position, _maxRangeFromCaster);
            Vector3 castPoint = executorData.Position + (Vector3)offset;

            List<IDamageable> targets = _detector.FindTargetsInRadius(castPoint);

            foreach (IDamageable target in targets)
            {
                if (target is not IKnockbackable knockbackable)
                    continue;

                Vector2 direction = (Vector2)(GetTargetPosition(knockbackable) - castPoint);
                knockbackable.ApplyKnockback(direction, _knockbackSpeedPerSecond, _knockbackDurationInSeconds);
            }

            yield return _instant;
        }

        private static Vector3 GetTargetPosition(IKnockbackable target)
        {
            return ((MonoBehaviour)target).transform.position;
        }
    }
}
