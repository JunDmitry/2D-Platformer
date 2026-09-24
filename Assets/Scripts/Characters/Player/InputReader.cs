using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode JumpKey = KeyCode.Space;
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode LeftKey = KeyCode.Mouse0;

    private static readonly KeyCode[] SkillKeys =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
    };

    private bool _isJump;
    private bool _isAttack;
    private bool[] _isSkill = new bool[SkillKeys.Length];

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(JumpKey))
            _isJump = true;

        if (Input.GetKeyDown(LeftKey))
            _isAttack = true;

        for (int i = 0; i < SkillKeys.Length; i++)
        {
            if (Input.GetKeyDown(SkillKeys[i]))
                _isSkill[i] = true;
        }
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

    public bool IsSkill(int index)
    {
        if (index < 0 || index >= _isSkill.Length)
            return false;

        bool isSkill = _isSkill[index];
        _isSkill[index] = false;

        return isSkill;
    }
}
