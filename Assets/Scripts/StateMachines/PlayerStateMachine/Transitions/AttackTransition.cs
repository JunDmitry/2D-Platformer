using UnityEngine;

public class AttackTransition : Transition
{
    private readonly InputReader _inputReader;
    private readonly float _attackSpeed;

    private float _nextAttackTime = float.Epsilon;

    public AttackTransition(State nextState, InputReader inputReader, float attackSpeed) : base(nextState)
    {
        _inputReader = inputReader;
        _attackSpeed = attackSpeed;
    }

    protected override bool CanTransit()
    {
        if (Time.time < _nextAttackTime
            || _inputReader.IsAttack() == false)
            return false;

        _nextAttackTime = Time.time + _attackSpeed;

        return true;
    }
}