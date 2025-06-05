using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Route))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyAnimationEvents _animation;

    private StateMachine _stateMachine;

    private void Awake()
    {
        Route route = GetComponent<Route>();
        Mover mover = GetComponent<Mover>();

        _stateMachine = new();

        State patrolState = new EnemyPatrolState(_stateMachine, mover, route, _animation);

        _stateMachine.ChangeState(patrolState);
        StartCoroutine(Sleep());
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    private IEnumerator Sleep(int countFrame = 2)
    {
        for (int i = 0; i < countFrame; i++)
            yield return null;
    }
}