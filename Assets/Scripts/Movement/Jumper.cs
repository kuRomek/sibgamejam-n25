using UnityEngine;

public class Jumper : MonoBehaviour
{
    private const float Gravity = -9.81f;
    private const KeyCode SpaceButton = KeyCode.Space;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _pivot;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _detectorGroundRadius = 0.4f;

    private float _defaultVelocity = -2;
    private float _velocity;
    private bool _isGrounded;

    private void FixedUpdate()
    {
        _isGrounded = IsGrounded();

        if (_isGrounded && _velocity < 0)
            _velocity = _defaultVelocity;

        DoGravity();
    }

    private void Update()
    {       
        if (Input.GetKeyDown(SpaceButton) && _isGrounded)
            Jump();
    }

    private void Jump() =>
        _velocity = Mathf.Sqrt(_jumpHeight * _defaultVelocity * Gravity);

    private void DoGravity()
    {
        _velocity += Gravity * Time.fixedDeltaTime;

        _characterController.Move(Vector3.up * _velocity * Time.fixedDeltaTime);
    }

    private bool IsGrounded()
    {
        bool result = Physics.CheckSphere(_pivot.position, _detectorGroundRadius, _groundMask);

        return result;
    }
}
