using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin[] _spawnPoints;
    [SerializeField] private float _secondsToRespawnAfterCollect;

    private WaitForSeconds _waitToActive;

    private void Awake()
    {
        _waitToActive = new(_secondsToRespawnAfterCollect);
    }

    private void Start()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
            _spawnPoints[i].Collected += OnCollected;
    }

    private void OnCollected(Coin coin)
    {
        coin.gameObject.SetActive(false);
        StartCoroutine(RespawnWithDelay(coin));
    }

    private IEnumerator RespawnWithDelay(Coin coin)
    {
        yield return _waitToActive;
        coin.gameObject.SetActive(true);
    }
}