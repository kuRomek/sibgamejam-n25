using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    private static Transform _transform;

    public static Vector3 Position => _transform.position;
    public static Collider Collider { get; private set; }
    public static bool IsInvisible { get; private set; }

    private void Awake()
    {
        _transform = transform;
        Collider = _collider;
    }

    public static void ToggleInvisible(bool isInvisible)
    {
        IsInvisible = isInvisible;
    }
}