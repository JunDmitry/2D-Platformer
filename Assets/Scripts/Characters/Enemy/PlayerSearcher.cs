using UnityEngine;

public class PlayerSearcher : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _range;

    public Transform Target { get; private set; }

    public bool IsFoundTarget => Target != null;

    public void Update()
    {
        Target = null;

        if (TryFindPlayer(Vector2.left, out Player player) == false
            && TryFindPlayer(Vector2.right, out player) == false)
            return;

        Target = player.transform;
    }

    private bool TryFindPlayer(Vector2 direction, out Player player)
    {
        player = default;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _range, _layer);

        if (hit.collider == null)
            return false;

        return hit.collider.TryGetComponent(out player);
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);

        Vector3 distance = new(_range, 0);
        Gizmos.DrawLine(transform.position, transform.position + distance);
        Gizmos.DrawLine(transform.position, transform.position - distance);
    }

#endif
}