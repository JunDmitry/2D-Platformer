using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Skill_System
{
    public class AuraSingleDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerToEffect;
        [SerializeField] private float _radius;

        public float Radius => _radius;

        public IEnumerable<IDamageable> FoundTargets(ExecutorData executorData)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(executorData.Position, _radius, _layerToEffect);
            float targetDistance = float.MaxValue;
            float distance;
            IDamageable target = default;

            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.TryGetComponent(out IDamageable damageable) == false)
                    continue;

                distance = (hit.transform.position - executorData.Position).sqrMagnitude;

                if (distance < targetDistance)
                {
                    targetDistance = distance;
                    target = damageable;
                }
            }

            yield return target;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}