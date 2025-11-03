using UnityEngine;

public class Jumper : MonoBehaviour
{
    private const KeyCode SpaceButton = KeyCode.Space;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _pivot;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _detectorGroundRadius = 0.4f;

    private float _defaultVelocity = -2;
    private bool _isGrounded;

    private void FixedUpdate()
    {
        _isGrounded = IsGrounded();

        if (_isGrounded && _rigidbody.linearVelocity.y < 0)
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, _defaultVelocity, _rigidbody.linearVelocity.z);
    }

    private void Update()
    {       
        if (Input.GetKeyDown(SpaceButton) && _isGrounded)
            Jump();
    }

    private void Jump() =>
        _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);

    private bool IsGrounded()
    {
        bool result = Physics.CheckSphere(_pivot.position, _detectorGroundRadius, _groundMask);

        return result;
    }
}
