using System;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Animator _weaponAnimator;

    private readonly int _directionHash = Animator.StringToHash("Direction");
    private readonly int _jumpHash = Animator.StringToHash("Jump");
    private readonly int _groundHash = Animator.StringToHash("IsGround");
    private readonly int _attackHash = Animator.StringToHash("Attack");
    private readonly int _dieHash = Animator.StringToHash("Die");

    public event Action OnDeath;

    public void SetDirection(float direction)
    {
        _playerAnimator.SetFloat(_directionHash, direction);
    }

    public void SetJump()
    {
        _playerAnimator.SetTrigger(_jumpHash);
    }

    public void SetOnGround()
    {
        _playerAnimator.SetBool(_groundHash, true);
    }

    public void SetInAir()
    {
        _playerAnimator.SetBool(_groundHash, false);
    }

    public void SetAttack()
    {
        _weaponAnimator.SetBool(_attackHash, true);
    }

    public void SetUnattack()
    {
        _weaponAnimator.SetBool(_attackHash, false);
    }

    public void SetDie()
    {
        _playerAnimator.SetTrigger(_dieHash);
    }

    public void InvokeDeathEvent()
    {
        OnDeath?.Invoke();
    }
}