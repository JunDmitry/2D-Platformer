public class EnemyStateMachineFactory
{
    public StateMachine Create(
        Mover mover,
        Route route,
        PlayerSearcher searcher,
        Attacker attacker,
        float attackSpeed,
        EnemyAnimationEvents animation)
    {
        StateMachine stateMachine = new();

        State patrolState = new EnemyPatrolState(stateMachine, mover, animation, route);
        State pursuitState = new EnemyPursuitState(stateMachine, mover, animation, searcher);
        State attackState = new EnemyAttackState(stateMachine, animation, attacker);

        Transition patrolTransition = new PatrolTransition(patrolState, searcher);
        Transition pursuitTransition = new PursuitTransition(pursuitState, searcher);
        Transition attackTransition = new EnemyAttackTransition(attackState, attackSpeed, attacker);

        patrolState.AddUpdateTransition(pursuitTransition);
        pursuitState.AddUpdateTransition(patrolTransition);
        pursuitState.AddUpdateTransition(attackTransition);
        attackState.AddUpdateTransition(pursuitTransition);
        attackState.AddUpdateTransition(patrolTransition);

        stateMachine.ChangeState(patrolState);

        return stateMachine;
    }
}