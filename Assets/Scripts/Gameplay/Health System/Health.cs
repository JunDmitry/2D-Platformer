using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealthChangeHandler
{
    [SerializeField, Min(1f)] private float _max = 1f;

    public event Action<float> CurrentChanged;

    public event Action<float> MaxChanged;

    public float Current { get; private set; }

    public float Max => _max;

    private void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        Current = _max;
        CurrentChanged?.Invoke(Current);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage cannot be less than 0!", nameof(damage));

        Current = Math.Max(Current - damage, 0);
        CurrentChanged?.Invoke(Current);
    }

    public void Replenish(float count)
    {
        if (count < 0)
            throw new ArgumentException("Replenish health count cannot be less than 0!", nameof(count));

        Current = Math.Min(Current + count, _max);
        CurrentChanged?.Invoke(Current);
    }
}