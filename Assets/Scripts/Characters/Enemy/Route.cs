using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    private readonly Vector2 _epsilon = new(0.1f, 0.5f);
    private int _pointIndex;

    private void Awake()
    {
        if (_points.Length == 0)
        {
            Debug.LogWarning($"Empty route!");
            _points = new Transform[] { transform };
        }
    }

    public void Reset()
    {
        _pointIndex = 0;
    }

    public Vector2 GetTarget()
    {
        if (IsArriveToTarget())
            _pointIndex = ++_pointIndex % _points.Length;

        return _points[_pointIndex].position;
    }

    private bool IsArriveToTarget()
    {
        Vector2 target = _points[_pointIndex].position;

        return Mathf.Abs(target.x - transform.position.x) < _epsilon.x
            && Mathf.Abs(target.y - transform.position.y) < _epsilon.y;
    }
}