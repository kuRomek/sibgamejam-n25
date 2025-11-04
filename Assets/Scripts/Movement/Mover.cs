using UnityEngine;

public class Mover : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    private float _directionY;
    private float _directionX;

    private Vector3 _direction;

    private void Update()
    {
        _directionY = Input.GetAxis(VerticalAxis);
        _directionX = Input.GetAxis(HorizontalAxis);

        _direction = new Vector3(_directionX, 0f, _directionY);

        transform.Translate(_speed * Time.deltaTime * _direction, Space.Self);
    }
}