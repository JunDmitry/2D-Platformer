using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode JumpKey = KeyCode.Space;
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode LeftKey = KeyCode.Mouse0;

    private bool _isJump;
    private bool _isAttack;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(JumpKey))
            _isJump = true;

        if (Input.GetKeyDown(LeftKey))
            _isAttack = true;
    }

    public bool IsJump()
    {
        bool isJump = _isJump;
        _isJump = false;

        return isJump;
    }

    public bool IsAttack()
    {
        bool isAttack = _isAttack;
        _isAttack = false;

        return isAttack;
    }
}