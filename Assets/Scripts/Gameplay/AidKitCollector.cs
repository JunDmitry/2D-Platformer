using UnityEngine;

[RequireComponent(typeof(IReplenishable))]
public class AidKitCollector : MonoBehaviour
{
    private IReplenishable _health;

    private void Awake()
    {
        _health = GetComponent<IReplenishable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out AidKit aidKit) == false)
            return;

        _health.Replenish(aidKit.HealthReplenish);
        aidKit.Interact();
    }
}