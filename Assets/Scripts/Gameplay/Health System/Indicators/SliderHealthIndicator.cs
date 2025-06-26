using UnityEngine;
using UnityEngine.UI;

public class SliderHealthIndicator : HealthIndicator
{
    [SerializeField] private Slider _sliderUI;

    protected override void OnCurrentHealthChanged(float value)
    {
        ChangeBarValue(value, MaxHealth);
    }

    protected override void OnMaxHealthChanged(float value)
    {
        ChangeBarValue(CurrentHealth, value);
    }

    private void ChangeBarValue(float current, float max)
    {
        float percentile = current / max;
        float range = _sliderUI.maxValue - _sliderUI.minValue;

        _sliderUI.value = _sliderUI.minValue + percentile * range;
    }
}