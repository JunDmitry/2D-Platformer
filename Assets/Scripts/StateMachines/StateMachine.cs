public class StateMachine : IStateChanger
{
    protected State _currentState;

    public void ChangeState(State nextState)
    {
        _currentState?.Exit();
        _currentState = nextState;
        _currentState?.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }

    public void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }
}