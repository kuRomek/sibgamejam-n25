using UnityEngine;

public class Rotator : MonoBehaviour
{
    private const string HorizontalMouseAxis = "Mouse X";
    private const string VerticalMouseAxis = "Mouse Y";

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _mouseSensetivityX;
    [SerializeField] private float _mouseSensetivityY;

    private float _rotationX = 0f;
    private float _minRotationX = -90f;
    private float _maxRotationX = 90f;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis(HorizontalMouseAxis) * _mouseSensetivityX * Time.deltaTime;
        float mouseY = Input.GetAxis(VerticalMouseAxis) * _mouseSensetivityY * Time.deltaTime;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, _minRotationX, _maxRotationX);

        _cameraTransform.localRotation = Quaternion.Euler(_rotationX, default, default);
        Quaternion deltaRotation = Quaternion.Euler(0f, mouseX, 0f);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
    }
}