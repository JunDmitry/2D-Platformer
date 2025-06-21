using UnityEngine;

public class EnemyPursuitState : EnemyMoveState
{
    private readonly PlayerSearcher _searcher;

    public EnemyPursuitState(IStateChanger stateMachine, Mover mover, EnemyAnimationEvents animation, PlayerSearcher searcher)
        : base(stateMachine, mover, animation)
    {
        _searcher = searcher;
    }

    protected override Vector2 GetTarget()
    {
        return _searcher.IsFoundTarget ? _searcher.Target.position : Mover.transform.position;
    }
}