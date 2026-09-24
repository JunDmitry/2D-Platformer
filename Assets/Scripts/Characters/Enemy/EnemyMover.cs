using System.Collections;
using UnityEngine;

public class EnemyMover : Mover
{
    private Coroutine _moveCoroutine;

    public override void Move(float direction)
    {
        if (IsOverridden)
            return;

        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        Vector2 target = new(transform.position.x + direction * MoveSpeed * Time.deltaTime, transform.position.y);
        _moveCoroutine = StartCoroutine(MoveTo(target));
    }

    public override void ApplyKnockback(Vector2 direction, float speedPerSecond, float durationInSeconds)
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = StartCoroutine(Knockback(direction, speedPerSecond, durationInSeconds));
    }

    public IEnumerator MoveTo(Vector3 target)
    {
        Vector3 source = transform.position;

        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(source, target, MoveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Knockback(Vector2 direction, float speedPerSecond, float durationInSeconds)
    {
        IsOverridden = true;
        Vector2 normalizedDirection = direction.normalized;
        float elapsedSeconds = 0f;

        while (elapsedSeconds < durationInSeconds)
        {
            transform.position += (Vector3)(normalizedDirection * speedPerSecond * Time.deltaTime);
            elapsedSeconds += Time.deltaTime;
            yield return null;
        }

        IsOverridden = false;
        _moveCoroutine = null;
    }
}
