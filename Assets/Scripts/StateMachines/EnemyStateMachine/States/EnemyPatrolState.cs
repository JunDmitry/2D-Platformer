using UnityEngine;

public class EnemyPatrolState : EnemyMoveState
{
    private readonly Route _route;

    public EnemyPatrolState(IStateChanger stateMachine, Mover mover, EnemyAnimationEvents animation, Route route)
        : base(stateMachine, mover, animation)
    {
        _route = route;
    }

    protected override Vector2 GetTarget()
    {
        return _route.GetTarget();
    }
}