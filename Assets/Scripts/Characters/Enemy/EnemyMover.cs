using System.Collections;
using UnityEngine;

public class EnemyMover : Mover
{
    private Coroutine _moveCoroutine;

    public override void Move(float direction)
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        Vector2 target = new(transform.position.x + direction * MoveSpeed * Time.deltaTime, transform.position.y);
        _moveCoroutine = StartCoroutine(MoveTo(target));
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
}