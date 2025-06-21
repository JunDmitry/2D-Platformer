using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Route), typeof(Health))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyAnimationEvents _animation;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private PlayerSearcher _playerSearcher;
    [SerializeField] private float _attackSpeed;

    private StateMachine _stateMachine;
    private Health _health;

    private void Awake()
    {
        _animation.OnDeath += () => Destroy(gameObject);

        _health = GetComponent<Health>();
        Route route = GetComponent<Route>();
        Mover mover = GetComponent<Mover>();

        EnemyStateMachineFactory factory = new();
        _stateMachine = factory.Create(mover, route, _playerSearcher, _attacker, _attackSpeed, _animation);

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

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);

        if (_health.Current == 0)
            _animation.SetDie();
    }

    private IEnumerator Sleep(int countFrame = 2)
    {
        for (int i = 0; i < countFrame; i++)
            yield return null;
    }
}