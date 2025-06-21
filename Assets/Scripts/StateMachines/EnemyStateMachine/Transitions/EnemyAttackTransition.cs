using System.Linq;
using UnityEngine;

public class EnemyAttackTransition : Transition
{
    private readonly float _attackSpeed;
    private readonly AttackDetector _detector;

    private float _nextAttackTime = float.Epsilon;

    public EnemyAttackTransition(State nextState, float attackSpeed, Attacker attacker) : base(nextState)
    {
        _attackSpeed = attackSpeed;
        _detector = attacker.GetComponent<AttackDetector>();
    }

    protected override bool CanTransit()
    {
        if (Time.time < _nextAttackTime || IsRequireAttack() == false)
            return false;

        _nextAttackTime = Time.time + _attackSpeed;

        return true;
    }

    private bool IsRequireAttack()
    {
        return _detector != null && _detector.GetTargets().Any();
    }
}