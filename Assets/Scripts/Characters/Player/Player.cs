using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Jumper), typeof(Health))]
public class Player : MonoBehaviour, IDamageable, IReplenishable
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerAnimationEvents _animation;

    [SerializeField] private Attacker _attacker;
    [SerializeField] private float _attackSpeed;

    private StateMachine _stateMachine;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        Mover mover = GetComponent<Mover>();
        Jumper jumper = GetComponent<Jumper>();

        PlayerStateMachineFactory factory = new();
        _stateMachine = factory.Create(_inputReader, mover, jumper, _animation, _attacker, _groundDetector, _attackSpeed);
    }

    private void OnEnable()
    {
        _animation.OnDeath += () => gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _animation.OnDeath -= () => gameObject.SetActive(false);
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

    public void Replenish(float count)
    {
        _health.Replenish(count);
    }
}