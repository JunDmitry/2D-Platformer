using Assets.Scripts.StateMachine.PlayerStateMachine;
using Assets.Scripts.StateMachine.PlayerStates;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Jumper))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerAnimationEvents _animation;

    private PlayerStateMachine _stateMachine;

    private void Start()
    {
        Mover mover = GetComponent<Mover>();
        Jumper jumper = GetComponent<Jumper>();

        _stateMachine = new();
        _stateMachine.AddState(new PlayerMovementState(_stateMachine, _inputReader, mover, _groundDetector, _animation));
        _stateMachine.AddState(new PlayerJumpState(_stateMachine, jumper, _animation));
        _stateMachine.AddState(new PlayerInAirState(_stateMachine, _inputReader, _groundDetector, _animation));

        _stateMachine.ChangeState(PlayerStateType.Move);
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