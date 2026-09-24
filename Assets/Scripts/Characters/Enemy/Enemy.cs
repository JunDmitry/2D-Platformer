using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Route), typeof(Health))]
public class Enemy : MonoBehaviour, IDamageable, IKnockbackable
{
    [SerializeField] private EnemyAnimationEvents _animation;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private PlayerSearcher _playerSearcher;
    [SerializeField] private float _attackSpeed;

    private StateMachine _stateMachine;
    private Health _health;
    private Mover _mover;

    private void Awake()
    {
        _health = GetComponent<Health>();
        Route route = GetComponent<Route>();
        _mover = GetComponent<Mover>();

        EnemyStateMachineFactory factory = new();
        _stateMachine = factory.Create(_mover, route, _playerSearcher, _attacker, _attackSpeed, _animation);

        StartCoroutine(Sleep());
    }

    private void OnEnable()
    {
        _animation.OnDeath += () => Destroy(gameObject);
    }

    private void OnDisable()
    {
        _animation.OnDeath -= () => Destroy(gameObject);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public float TakeDamage(float damage)
    {
        float oldHealth = _health.Current;
        _health.TakeDamage(damage);

        if (_health.Current == 0)
            _animation.SetDie();

        return oldHealth - _health.Current;
    }

    public void ApplyKnockback(Vector2 direction, float speedPerSecond, float durationInSeconds)
    {
        _mover.ApplyKnockback(direction, speedPerSecond, durationInSeconds);
    }

    private IEnumerator Sleep(int countFrame = 2)
    {
        for (int i = 0; i < countFrame; i++)
            yield return null;
    }
}