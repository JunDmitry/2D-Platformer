using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int _directionHash = Animator.StringToHash("Direction");

    public void SetDirection(float direction)
    {
        _animator.SetFloat(_directionHash, direction);
    }
}