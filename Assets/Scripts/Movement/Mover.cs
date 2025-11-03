using UnityEngine;

public class Mover : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    private void Update()
    {
        float directionY = Input.GetAxis(VerticalAxis);
        float directionX = Input.GetAxis(HorizontalAxis);

        Vector3 moveDirection = (transform.forward * directionY + transform.right * directionX).normalized * _speed;

        Vector3 currentVelocity = _rigidbody.linearVelocity;
        _rigidbody.linearVelocity = new Vector3(moveDirection.x, currentVelocity.y, moveDirection.z);
    }

    private void FixedUpdate()
    {
        float directionY = Input.GetAxis(VerticalAxis);
        float directionX = Input.GetAxis(HorizontalAxis);

        Vector3 moveDirection = (transform.forward * directionY + transform.right * directionX).normalized * _speed;

        float currentVelocityY = _rigidbody.linearVelocity.y;
        _rigidbody.linearVelocity = new Vector3(moveDirection.x, currentVelocityY, moveDirection.z);
    }
}