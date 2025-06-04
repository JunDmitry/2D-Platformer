using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int _directionHash = Animator.StringToHash("Direction");
    private readonly int _jumpHash = Animator.StringToHash("Jump");
    private readonly int _groundHash = Animator.StringToHash("IsGround");

    public void SetDirection(float direction)
    {
        _animator.SetFloat(_directionHash, direction);
    }

    public void SetJump()
    {
        _animator.SetTrigger(_jumpHash);
    }

    public void SetOnGround()
    {
        _animator.SetBool(_groundHash, true);
    }

    public void SetInAir()
    {
        _animator.SetBool(_groundHash, false);
    }
}