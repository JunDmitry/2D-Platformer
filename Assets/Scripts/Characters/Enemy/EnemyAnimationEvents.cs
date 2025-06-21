using System;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int _directionHash = Animator.StringToHash("Direction");
    private readonly int _dieHash = Animator.StringToHash("Die");
    private readonly int _attackHash = Animator.StringToHash("Attack");

    public event Action OnDeath;

    public void SetDirection(float direction)
    {
        _animator.SetFloat(_directionHash, direction);
    }

    public void SetAttack()
    {
        _animator.SetBool(_attackHash, true);
    }

    public void SetUnattack()
    {
        _animator.SetBool(_attackHash, false);
    }

    public void SetDie()
    {
        _animator.SetTrigger(_dieHash);
    }

    public void InvokeDeathEvent()
    {
        OnDeath?.Invoke();
    }
}