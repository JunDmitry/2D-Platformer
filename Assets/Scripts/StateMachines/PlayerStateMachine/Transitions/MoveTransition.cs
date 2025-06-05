public class MoveTransition : Transition
{
    private readonly GroundDetector _groundDetector;

    public MoveTransition(State nextState, GroundDetector groundDetector) : base(nextState)
    {
        _groundDetector = groundDetector;
    }

    protected override bool CanTransit()
    {
        return _groundDetector.IsGround;
    }
}