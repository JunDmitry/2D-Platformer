namespace Assets.Scripts.StateMachine.PlayerStates
{
    public class PlayerInAirState : PlayerState
    {
        private readonly InputReader _inputReader;
        private readonly GroundDetector _groundDetector;
        private readonly PlayerAnimationEvents _animation;

        public PlayerInAirState(StateMachine<PlayerStateType> stateMachine, InputReader inputReader, GroundDetector groundDetector, PlayerAnimationEvents animation)
            : base(stateMachine, PlayerStateType.InAir)
        {
            _inputReader = inputReader;
            _groundDetector = groundDetector;
            _animation = animation;
        }

        public override void Enter()
        {
            base.Enter();

            _animation.SetInAir();
        }

        public override void Update()
        {
            if (_groundDetector.IsGround)
                StateMachine.ChangeState(PlayerStateType.Move);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            _animation.SetDirection(_inputReader.Direction);
        }
    }
}