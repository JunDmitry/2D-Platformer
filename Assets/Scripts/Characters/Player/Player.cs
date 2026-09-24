using Assets.Scripts.Gameplay.Skill_System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Jumper), typeof(Health))]
public class Player : MonoBehaviour, IDamageable, IReplenishable, IKnockbackable
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerAnimationEvents _animation;

    [SerializeField] private Skill[] _skills;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private float _attackSpeed;

    private StateMachine _stateMachine;
    private Health _health;
    private Mover _mover;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
        Jumper jumper = GetComponent<Jumper>();

        PlayerStateMachineFactory factory = new();
        _stateMachine = factory.Create(_inputReader, _mover, jumper, _animation, _attacker, _groundDetector, _attackSpeed);
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
        for (int i = 0; i < _skills.Length; i++)
        {
            if (_inputReader.IsSkill(i))
                _skills[i].Execute(new(gameObject, this, this));
        }

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

    public void Replenish(float count)
    {
        _health.Replenish(count);
    }

    public void ApplyKnockback(Vector2 direction, float speedPerSecond, float durationInSeconds)
    {
        _mover.ApplyKnockback(direction, speedPerSecond, durationInSeconds);
    }
}