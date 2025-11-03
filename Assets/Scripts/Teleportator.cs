using UnityEngine;

public class Teleportator : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _point;

    public void Teleport()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.transform.position = _point.position;
        _rigidbody.isKinematic = false;
    }
}
