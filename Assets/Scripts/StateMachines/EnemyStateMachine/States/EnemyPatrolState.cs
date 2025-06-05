using UnityEngine;

public class EnemyPatrolState : State
{
    private const float LeftDirection = -1f;
    private const float RightDirection = 1f;

    private readonly Mover _mover;
    private readonly Route _route;
    private readonly EnemyAnimationEvents _animation;

    public EnemyPatrolState(IStateChanger stateMachine, Mover mover, Route route, EnemyAnimationEvents animation)
        : base(stateMachine)
    {
        _mover = mover;
        _route = route;
        _animation = animation;
    }

    protected override void OnUpdating()
    {
        base.OnUpdating();

        float positionX = _mover.transform.position.x;
        Vector2 target = _route.GetTarget();
        float direction = (target.x - positionX) < 0 ? LeftDirection : RightDirection;

        _animation.SetDirection(direction);
        _mover.Move(direction);
    }
}