using UnityEngine;

public class Mover : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _steps;

    private Vector3 _direction;
    private float _accumTime = 0;
    private float _secondsForStep = 0.5f;

    private void Update()
    {
        _direction = new Vector3(Input.GetAxis(HorizontalAxis), 0f, Input.GetAxis(VerticalAxis));

        if (_direction != Vector3.zero)
        {
            _accumTime += Time.deltaTime;

            if (_accumTime > _secondsForStep && Jumper._isGrounded)
            {
                _accumTime = 0;
                _audioSource.PlayOneShot(_steps[Random.Range(0, _steps.Length)]);
            }
        }

        transform.Translate(_speed * Time.deltaTime * _direction, Space.Self);
    }
}