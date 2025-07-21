using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode JumpKey = KeyCode.Space;
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode LeftKey = KeyCode.Mouse0;
    private const KeyCode OneKey = KeyCode.Alpha1;

    private bool _isJump;
    private bool _isAttack;
    private bool _isFirstSkill;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(JumpKey))
            _isJump = true;

        if (Input.GetKeyDown(LeftKey))
            _isAttack = true;

        if (Input.GetKeyDown(OneKey))
            _isFirstSkill = true;
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

    public bool IsFirstSkill()
    {
        bool isFirstSkill = _isFirstSkill;
        _isFirstSkill = false;

        return isFirstSkill;
    }
}