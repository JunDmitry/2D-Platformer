using TMPro;
using UnityEngine;

public class TextHealthIndicator : HealthIndicator
{
    [SerializeField] private TextMeshProUGUI _textUI;

    protected override void OnCurrentHealthChanged(float value)
    {
        ChangeText(value, MaxHealth);
    }

    protected override void OnMaxHealthChanged(float value)
    {
        ChangeText(CurrentHealth, value);
    }

    private void ChangeText(float current, float max)
    {
        _textUI.text = $"{current:F0}/{max:F0}";
    }
}