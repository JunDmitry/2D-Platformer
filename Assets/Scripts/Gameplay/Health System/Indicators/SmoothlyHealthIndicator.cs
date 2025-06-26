using System;
using System.Collections;
using UnityEngine;

public class SmoothlyHealthIndicator : SliderHealthIndicator
{
    [SerializeField, Min(0)] private float _smoothDuration = 0.25f;

    private float _current;
    private float _max;

    private Coroutine _coroutine;

    public float Current
    {
        get { return _current; }
        private set
        {
            _current = value;
            base.OnCurrentHealthChanged(value);
        }
    }

    public float Max
    {
        get { return _max; }
        private set
        {
            _max = value;
            base.OnMaxHealthChanged(value);
        }
    }

    private void Start()
    {
        _current = CurrentHealth;
        _max = MaxHealth;
    }

    protected override void OnCurrentHealthChanged(float value)
    {
        if (_coroutine != null)
            StopCoroutine();

        Max = MaxHealth;
        _coroutine = StartCoroutine(SmoothlyChangeValue(Current, CurrentHealth, (value) => Current = value));
    }

    protected override void OnMaxHealthChanged(float value)
    {
        if (_coroutine != null)
            StopCoroutine();

        Current = CurrentHealth;
        _coroutine = StartCoroutine(SmoothlyChangeValue(Max, MaxHealth, (value) => Max = value));
    }

    private IEnumerator SmoothlyChangeValue(float source, float target, Action<float> changeEvent)
    {
        float elapsedSeconds = 0f;
        float nextValue;

        while (elapsedSeconds < _smoothDuration)
        {
            elapsedSeconds += Time.deltaTime;
            nextValue = Mathf.Lerp(source, target, elapsedSeconds / _smoothDuration);
            changeEvent?.Invoke(nextValue);

            yield return null;
        }
    }

    private void StopCoroutine()
    {
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}