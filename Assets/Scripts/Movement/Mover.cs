using UnityEngine;

public class Mover : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed;

    private void Update()
    {
        float directionY = Input.GetAxis(VerticalAxis);
        float directionX = Input.GetAxis(HorizontalAxis);

        _characterController.Move((_characterController.transform.forward * directionY + _characterController.transform.right * directionX) * _speed * Time.deltaTime);
    }
}