using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private AttackDetector _detector;
    [SerializeField] private float _attackDamage;

    public void Attack()
    {
        IEnumerable<IDamageable> targets = _detector.GetTargets();

        foreach (IDamageable target in targets)
            target.TakeDamage(_attackDamage);
    }
}