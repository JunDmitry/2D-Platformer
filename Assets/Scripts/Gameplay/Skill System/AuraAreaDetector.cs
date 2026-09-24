using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Skill_System
{
    public class AuraAreaDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerToEffect;
        [SerializeField] private float _radius;

        public float Radius => _radius;

        public List<IDamageable> FindTargetsInRadius(ExecutorData executorData)
        {
            return FindTargetsInRadius(executorData.Position);
        }

        public List<IDamageable> FindTargetsInRadius(Vector3 center)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(center, _radius, _layerToEffect);
            List<IDamageable> targets = new(hits.Length);

            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.TryGetComponent(out IDamageable damageable))
                    targets.Add(damageable);
            }

            return targets;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
