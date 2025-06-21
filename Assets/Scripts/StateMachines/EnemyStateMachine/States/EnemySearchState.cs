public class EnemySearchState : State
{
    private readonly EnemyAnimationEvents _animation;

    public EnemySearchState(IStateChanger stateMachine, EnemyAnimationEvents animation) : base(stateMachine)
    {
        _animation = animation;
    }

    public override void Enter()
    {
        base.Enter();

        _animation.SetDirection(0f);
    }
}