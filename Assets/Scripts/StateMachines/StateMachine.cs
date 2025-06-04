using System.Collections.Generic;

public class StateMachine<T>
{
    protected Dictionary<T, State<T>> States;
    protected State<T> CurrentState;

    public StateMachine()
    {
        States = new();
    }

    public void AddState(State<T> state)
    {
        States[state.Id] = state;
    }

    public bool TryGetState(T id, out State<T> state)
    {
        state = GetState(id);

        return state != null;
    }

    public State<T> GetState(T id)
    {
        States.TryGetValue(id, out State<T> state);

        return state;
    }

    public void ChangeState(T id)
    {
        CurrentState?.Exit();
        CurrentState = GetState(id);
        CurrentState?.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }

    public void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }
}