public class PatrolTransition : Transition
{
    private readonly PlayerSearcher _searcher;

    public PatrolTransition(State nextState, PlayerSearcher searcher) : base(nextState)
    {
        _searcher = searcher;
    }

    protected override bool CanTransit()
    {
        return _searcher.IsFoundTarget == false;
    }
}