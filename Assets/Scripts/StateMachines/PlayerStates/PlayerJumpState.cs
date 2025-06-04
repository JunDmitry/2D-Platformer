using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStates
{
    public class PlayerJumpState : PlayerState
    {
        private readonly Jumper _jumper;
        private readonly PlayerAnimationEvents _animation;

        public PlayerJumpState(StateMachine<PlayerStateType> stateMachine, Jumper jumper, PlayerAnimationEvents animation)
            : base(stateMachine, PlayerStateType.Jump)
        {
            _jumper = jumper;
            _animation = animation;
        }

        public override void Enter()
        {
            base.Enter();

            _animation.SetJump();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            Debug.Log("Jump");
            _jumper.Jump();
            StateMachine.ChangeState(PlayerStateType.InAir);
        }
    }
}