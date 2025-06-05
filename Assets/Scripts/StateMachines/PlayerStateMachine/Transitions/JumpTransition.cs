public class JumpTransition : Transition
{
    private readonly InputReader _inputReader;
    private readonly GroundDetector _groundDetector;

    public JumpTransition(State nextState, InputReader inputReader, GroundDetector groundDetector) : base(nextState)
    {
        _inputReader = inputReader;
        _groundDetector = groundDetector;
    }

    protected override bool CanTransit()
    {
        return _inputReader.IsJump() && _groundDetector.IsGround;
    }
}