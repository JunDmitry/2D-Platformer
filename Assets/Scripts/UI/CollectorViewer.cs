using TMPro;
using UnityEngine;

public class CollectorViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private CoinCollector _collector;

    private void Awake()
    {
        _text.text = "Coins: 0";
    }

    private void OnEnable()
    {
        _collector.ChangedCount += OnChangedCount;
    }

    private void OnDisable()
    {
        _collector.ChangedCount -= OnChangedCount;
    }

    private void OnChangedCount(int count)
    {
        _text.text = $"Coins: {count}";
    }
}