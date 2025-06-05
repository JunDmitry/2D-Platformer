namespace Assets.Scripts.StateMachine.PlayerStates
{
    public class PlayerMovementState : State
    {
        private readonly InputReader _inputReader;
        private readonly Mover _mover;
        private readonly PlayerAnimationEvents _animation;

        public PlayerMovementState(IStateChanger stateMachine, InputReader inputReader, Mover mover, PlayerAnimationEvents animation)
                : base(stateMachine)
        {
            _inputReader = inputReader;
            _mover = mover;
            _animation = animation;
        }

        public override void Enter()
        {
            base.Enter();

            _animation.SetOnGround();
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            _animation.SetDirection(_inputReader.Direction);

            if (_inputReader.Direction != 0f)
                _mover.Move(_inputReader.Direction);
        }
    }
}