using UnityEngine;

public abstract class EnemyMoveState : State
{
    private const float Epsilon = 1e-3f;

    protected readonly Mover Mover;
    protected readonly EnemyAnimationEvents Animation;

    public EnemyMoveState(IStateChanger stateMachine, Mover mover, EnemyAnimationEvents animation) : base(stateMachine)
    {
        Mover = mover;
        Animation = animation;
    }

    protected override void OnUpdating()
    {
        base.OnUpdating();

        float positionX = Mover.transform.position.x;
        Vector2 target = GetTarget();
        float distance = target.x - positionX;
        float direction = distance < 0 ? Direction.Left : Direction.Right;

        if (Mathf.Approximately(Mathf.Abs(distance), Epsilon))
            direction = 0f;

        Animation.SetDirection(direction);

        if (direction != 0f)
            Mover.Move(direction);
    }

    protected abstract Vector2 GetTarget();
}