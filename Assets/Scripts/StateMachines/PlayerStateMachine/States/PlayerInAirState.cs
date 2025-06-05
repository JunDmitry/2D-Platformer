namespace Assets.Scripts.StateMachine.PlayerStates
{
    public class PlayerInAirState : State
    {
        private readonly InputReader _inputReader;
        private readonly PlayerAnimationEvents _animation;

        public PlayerInAirState(IStateChanger stateMachine, InputReader inputReader, PlayerAnimationEvents animation)
            : base(stateMachine)
        {
            _inputReader = inputReader;
            _animation = animation;
        }

        public override void Enter()
        {
            base.Enter();

            _animation.SetInAir();
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            _animation.SetDirection(_inputReader.Direction);
        }
    }
}