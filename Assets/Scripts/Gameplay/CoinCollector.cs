using System;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private int _coinsValue;

    public event Action<int> ChangedCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin) == false)
            return;

        _coinsValue += coin.Value;
        ChangedCount?.Invoke(_coinsValue);
        coin.Interact();
    }
}