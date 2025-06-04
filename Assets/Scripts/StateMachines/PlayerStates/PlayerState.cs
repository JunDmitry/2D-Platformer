public abstract class PlayerState : State<PlayerStateType>
{
    protected PlayerState(StateMachine<PlayerStateType> stateMachine, PlayerStateType id)
        : base(stateMachine, id)
    {
    }
}