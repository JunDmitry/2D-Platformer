using UnityEngine;

public class PlayerMover : Mover
{
    public override void Move(float direction)
    {
        Rigidbody.velocity = new Vector2(direction * MoveSpeed * Time.fixedDeltaTime, Rigidbody.velocity.y);
    }
}