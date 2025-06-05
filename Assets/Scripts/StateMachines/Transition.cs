public abstract class Transition
{
    protected readonly State NextState;

    protected Transition(State nextState)
    {
        NextState = nextState;
    }

    public bool TryTransit(out State nextState)
    {
        nextState = default;

        if (CanTransit())
        {
            nextState = NextState;
            return true;
        }

        return false;
    }

    protected abstract bool CanTransit();
}