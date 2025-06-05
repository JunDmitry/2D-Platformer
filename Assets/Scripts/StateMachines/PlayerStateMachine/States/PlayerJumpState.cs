namespace Assets.Scripts.StateMachine.PlayerStates
{
    public class PlayerJumpState : State
    {
        private readonly Jumper _jumper;
        private readonly PlayerAnimationEvents _animation;

        public PlayerJumpState(IStateChanger stateMachine, Jumper jumper, PlayerAnimationEvents animation)
            : base(stateMachine)
        {
            _jumper = jumper;
            _animation = animation;
        }

        public override void Enter()
        {
            base.Enter();

            _animation.SetJump();
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            _jumper.Jump();
        }
    }
}