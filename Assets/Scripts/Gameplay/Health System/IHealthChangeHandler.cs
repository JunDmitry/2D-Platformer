using System;

public interface IHealthChangeHandler
{
    event Action<float> CurrentChanged;

    event Action<float> MaxChanged;

    float Current { get; }
    float Max { get; }
}