public class InAirTransition : Transition
{
    private readonly GroundDetector _groundDetector;

    public InAirTransition(State nextState, GroundDetector groundDetector) : base(nextState)
    {
        _groundDetector = groundDetector;
    }

    protected override bool CanTransit()
    {
        return _groundDetector.IsGround == false;
    }
}