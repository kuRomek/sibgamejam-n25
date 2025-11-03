using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] private Teleportator _teleportator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
            _teleportator.Teleport();
    }
}