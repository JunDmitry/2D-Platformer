public abstract class State<T>
{
    protected StateMachine<T> StateMachine;

    protected State(StateMachine<T> stateMachine, T id)
    {
        StateMachine = stateMachine;
        Id = id;
    }

    public T Id { get; }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }
}