using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed;

    private float _directionY;
    private float _directionX;

    private void Update()
    {
        _directionY = Input.GetAxis("Vertical");
        _directionX = Input.GetAxis("Horizontal");

        _characterController.Move((Vector3.forward * _directionY + Vector3.right * _directionX) * _speed * Time.deltaTime);
    }
}