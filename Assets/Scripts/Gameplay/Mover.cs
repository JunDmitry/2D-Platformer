using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Mover : MonoBehaviour
{
    [SerializeField] protected float MoveSpeed;

    protected Rigidbody2D Rigidbody;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public abstract void Move(float direction);
}