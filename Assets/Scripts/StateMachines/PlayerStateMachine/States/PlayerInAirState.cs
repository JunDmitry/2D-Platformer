namespace Assets.Scripts.StateMachine.PlayerStates
{
    public class PlayerInAirState : State
    {
        private readonly InputReader _inputReader;
        private readonly PlayerAnimationEvents _animation;
        private readonly Mover _mover;

        public PlayerInAirState(IStateChanger stateMachine, InputReader inputReader, PlayerAnimationEvents animation, Mover mover)
            : base(stateMachine)
        {
            _inputReader = inputReader;
            _animation = animation;
            _mover = mover;
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
            _mover.Move(_inputReader.Direction);
        }
    }
}