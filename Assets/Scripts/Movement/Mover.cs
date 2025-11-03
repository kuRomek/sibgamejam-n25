using UnityEngine;

public class Mover : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    private float _directionY;
    private float _directionX;

    private void Update()
    {
        _directionY = Input.GetAxis(VerticalAxis);
        _directionX = Input.GetAxis(HorizontalAxis);
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = (transform.forward * _directionY + transform.right * _directionX).normalized * _speed;

        float currentVelocityY = _rigidbody.linearVelocity.y;
        _rigidbody.linearVelocity = new Vector3(moveDirection.x, currentVelocityY, moveDirection.z);
    }
}