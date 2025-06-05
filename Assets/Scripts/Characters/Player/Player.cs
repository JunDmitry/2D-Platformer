using Assets.Scripts.StateMachine.PlayerStates;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Jumper))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerAnimationEvents _animation;

    private StateMachine _stateMachine;

    private void Start()
    {
        Mover mover = GetComponent<Mover>();
        Jumper jumper = GetComponent<Jumper>();

        _stateMachine = new();

        State moveState = new PlayerMovementState(_stateMachine, _inputReader, mover, _animation);
        State jumpState = new PlayerJumpState(_stateMachine, jumper, _animation);
        State inAirState = new PlayerInAirState(_stateMachine, _inputReader, _animation);

        Transition inAirTransition = new InAirTransition(inAirState, _groundDetector);
        Transition jumpTransition = new JumpTransition(jumpState, _inputReader, _groundDetector);
        Transition moveTransition = new MoveTransition(moveState, _groundDetector);

        moveState.AddUpdateTransition(inAirTransition);
        moveState.AddFixedUpdateTransition(jumpTransition);
        jumpState.AddFixedUpdateTransition(inAirTransition);
        inAirState.AddUpdateTransition(moveTransition);

        _stateMachine.ChangeState(moveState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }
}