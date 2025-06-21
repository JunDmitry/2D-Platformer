using Assets.Scripts.StateMachine.PlayerStates;

public class PlayerStateMachineFactory
{
    public StateMachine Create(
        InputReader inputReader,
        Mover mover,
        Jumper jumper,
        PlayerAnimationEvents animation,
        Attacker attacker,
        GroundDetector groundDetector,
        float attackSpeed)
    {
        StateMachine stateMachine = new();

        State moveState = new PlayerMovementState(stateMachine, inputReader, mover, animation);
        State jumpState = new PlayerJumpState(stateMachine, jumper, animation);
        State inAirState = new PlayerInAirState(stateMachine, inputReader, animation, mover);
        State attackState = new PlayerAttackState(stateMachine, attacker, animation);

        Transition inAirTransition = new InAirTransition(inAirState, groundDetector);
        Transition jumpTransition = new JumpTransition(jumpState, inputReader, groundDetector);
        Transition moveTransition = new MoveTransition(moveState, groundDetector);
        Transition attackTransition = new AttackTransition(attackState, inputReader, attackSpeed);

        moveState.AddUpdateTransition(inAirTransition);
        moveState.AddFixedUpdateTransition(jumpTransition);
        moveState.AddUpdateTransition(attackTransition);
        jumpState.AddFixedUpdateTransition(inAirTransition);
        inAirState.AddUpdateTransition(moveTransition);
        attackState.AddUpdateTransition(moveTransition);
        attackState.AddUpdateTransition(jumpTransition);
        attackState.AddUpdateTransition(inAirTransition);

        stateMachine.ChangeState(moveState);

        return stateMachine;
    }
}