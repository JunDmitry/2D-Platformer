public class EnemyAttackState : State
{
    private readonly EnemyAnimationEvents _animation;
    private readonly Attacker _attacker;

    public EnemyAttackState(IStateChanger stateMachine, EnemyAnimationEvents animation, Attacker attacker) : base(stateMachine)
    {
        _animation = animation;
        _attacker = attacker;
    }

    public override void Enter()
    {
        base.Enter();

        _animation.SetAttack();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        _attacker.Attack();
    }

    public override void Exit()
    {
        base.Exit();

        _animation.SetUnattack();
    }
}