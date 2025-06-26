using UnityEngine;

public abstract class HealthIndicator : MonoBehaviour
{
    [SerializeField] private Health _health;

    protected float CurrentHealth => _health.Current;
    protected float MaxHealth => _health.Max;

    protected virtual void OnEnable()
    {
        _health.CurrentChanged += OnCurrentHealthChanged;
        _health.MaxChanged += OnMaxHealthChanged;
    }

    protected virtual void OnDisable()
    {
        _health.CurrentChanged -= OnCurrentHealthChanged;
        _health.MaxChanged -= OnMaxHealthChanged;
    }

    protected abstract void OnCurrentHealthChanged(float value);

    protected abstract void OnMaxHealthChanged(float value);
}