using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Lava : MonoBehaviour
{
    [SerializeField] private Teleportator _teleportator;
    [SerializeField] private Image _panel;
    [SerializeField] private Transform _flashlight;
    [SerializeField] private Transform _overheating;

    private float _speed = 20f;
    private WaitForFixedUpdate _waitForFixedUpdate;

    private void Awake()
    {
        _waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
            StartCoroutine(nameof(CloseEyes));
    }

    private IEnumerator CloseEyes()
    {
        _flashlight.gameObject.SetActive(false);
        _overheating.gameObject.SetActive(false);

        while (_panel.color.a < 1f)
        {
            _panel.color = new Color(0, 0, 0, _panel.color.a + 0.1f * _speed * Time.deltaTime);

            yield return _waitForFixedUpdate;
        }

        _teleportator.Teleport();

        _flashlight.gameObject.SetActive(true);
        _overheating.gameObject.SetActive(true);

        while (_panel.color.a > 0f)
        {
            _panel.color = new Color(0, 0, 0, _panel.color.a - 0.1f * _speed * Time.deltaTime);

            yield return _waitForFixedUpdate;
        }
    }
}