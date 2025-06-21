using System.Collections.Generic;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField, Min(0)] private float _range;
    [SerializeField] private Vector3 _offset;

    public IEnumerable<IDamageable> GetTargets()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position + _offset, _range, _layer);

        foreach (Collider2D hit in hits)
        {
            if (hit == null || hit.gameObject == null)
                continue;

            if (hit.gameObject.TryGetComponent(out IDamageable damageable))
                yield return damageable;
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position + _offset, _range);
    }

#endif
}