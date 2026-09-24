using System.Collections;
using UnityEngine;

public class PlayerMover : Mover
{
    private Coroutine _knockbackCoroutine;

    public override void Move(float direction)
    {
        if (IsOverridden)
            return;

        Rigidbody.velocity = new Vector2(direction * MoveSpeed * Time.fixedDeltaTime, Rigidbody.velocity.y);
    }

    public override void ApplyKnockback(Vector2 direction, float speedPerSecond, float durationInSeconds)
    {
        if (_knockbackCoroutine != null)
            StopCoroutine(_knockbackCoroutine);

        _knockbackCoroutine = StartCoroutine(Knockback(direction, speedPerSecond, durationInSeconds));
    }

    private IEnumerator Knockback(Vector2 direction, float speedPerSecond, float durationInSeconds)
    {
        IsOverridden = true;
        Vector2 velocity = direction.normalized * speedPerSecond;
        float elapsedSeconds = 0f;

        while (elapsedSeconds < durationInSeconds)
        {
            Rigidbody.velocity = velocity;
            elapsedSeconds += Time.deltaTime;
            yield return null;
        }

        IsOverridden = false;
        _knockbackCoroutine = null;
    }
}
