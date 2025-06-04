namespace Assets.Scripts.StateMachine.PlayerStates
{
    public class PlayerMovementState : PlayerState
    {
        private readonly InputReader _inputReader;
        private readonly Mover _mover;
        private readonly GroundDetector _groundDetector;
        private readonly PlayerAnimationEvents _animation;

        public PlayerMovementState(
            StateMachine<PlayerStateType> stateMachine, InputReader inputReader,
            Mover mover, GroundDetector groundDetector, PlayerAnimationEvents animation)
                : base(stateMachine, PlayerStateType.Move)
        {
            _inputReader = inputReader;
            _mover = mover;
            _groundDetector = groundDetector;
            _animation = animation;
        }

        public override void Enter()
        {
            base.Enter();

            _animation.SetOnGround();
        }

        public override void Update()
        {
            base.Update();

            if (_groundDetector.IsGround == false)
                StateMachine.ChangeState(PlayerStateType.InAir);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            _animation.SetDirection(_inputReader.Direction);

            if (_inputReader.Direction != 0f)
                _mover.Move(_inputReader.Direction);

            if (_inputReader.IsJump() && _groundDetector.IsGround)
                StateMachine.ChangeState(PlayerStateType.Jump);
        }
    }
}