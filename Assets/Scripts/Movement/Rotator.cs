using UnityEngine;

public class Rotator : MonoBehaviour
{
    private const string HorizontalMouseAxis = "Mouse X";
    private const string VerticalMouseAxis = "Mouse Y";

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _mouseSensetivityX;
    [SerializeField] private float _mouseSensetivityY;

    private float _rotationX = 0f;

    private void Update()
    {
        float mouseX = Input.GetAxis(HorizontalMouseAxis) * _mouseSensetivityX * Time.deltaTime;
        float mouseY = Input.GetAxis(VerticalMouseAxis) * _mouseSensetivityY * Time.deltaTime;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        _cameraTransform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        _characterController.transform.Rotate(Vector3.up * mouseX); 
    }
}