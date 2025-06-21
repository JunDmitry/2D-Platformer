public class PursuitTransition : Transition
{
    private readonly PlayerSearcher _searcher;

    public PursuitTransition(State nextState, PlayerSearcher searcher) : base(nextState)
    {
        _searcher = searcher;
    }

    protected override bool CanTransit()
    {
        return _searcher.IsFoundTarget;
    }
}