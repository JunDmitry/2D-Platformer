using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private readonly Vector3 _rotation = new(180, 0, 180);

    private bool _isFlipped = false;

    private IOffsetChanger _offsetChanger;

    private void Awake()
    {
        _offsetChanger = GetComponent<IOffsetChanger>();
    }

    private void Update()
    {
        if (IsRequireFlip() == false)
            return;

        _isFlipped = _isFlipped == false;
        Flip();
    }

    private bool IsRequireFlip()
    {
        return (_inputReader.Direction >= 0) == _isFlipped;
    }

    private void Flip()
    {
        Vector2 position = transform.localPosition;
        position.x *= -1;

        _offsetChanger?.ChangeOffsetDirection(Vector2.left + Vector2.up);
        transform.Rotate(_rotation, Space.Self);
        transform.localPosition = position;
    }
}