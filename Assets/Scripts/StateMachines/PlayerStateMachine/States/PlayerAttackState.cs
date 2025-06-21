public class PlayerAttackState : State
{
    private readonly Attacker _attacker;
    private readonly PlayerAnimationEvents _animation;

    public PlayerAttackState(IStateChanger stateMachine, Attacker attacker, PlayerAnimationEvents animation) : base(stateMachine)
    {
        _attacker = attacker;
        _animation = animation;
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