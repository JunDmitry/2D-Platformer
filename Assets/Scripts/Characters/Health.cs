using System;
using UnityEngine;

public class Health : MonoBehaviour, IReplenishable
{
    [SerializeField, Min(1f)] private float _max = 1f;

    private float _current;

    public float Current => _current;

    private void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        _current = _max;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage cannot be less than 0!", nameof(damage));

        _current = Math.Max(_current - damage, 0);
    }

    public void Replenish(float count)
    {
        if (count < 0)
            throw new ArgumentException("Replenish health count cannot be less than 0!", nameof(count));

        _current = Math.Min(_current + count, _max);
    }
}