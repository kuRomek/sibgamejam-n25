using UnityEngine;

public class Teleportator : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _point;

    public void Teleport()
    {
        _characterController.enabled = false;
        _characterController.transform.position = _point.position;
        _characterController.enabled = true;
    }
}
