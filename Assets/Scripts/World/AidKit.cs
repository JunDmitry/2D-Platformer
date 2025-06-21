using UnityEngine;

public class AidKit : MonoBehaviour, IIntractable
{
    [SerializeField] private float _healthReplenish;

    public float HealthReplenish => _healthReplenish;

    public void Interact()
    {
        Destroy(gameObject);
    }
}