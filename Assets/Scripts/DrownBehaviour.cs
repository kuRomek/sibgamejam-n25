using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DrownBehaviour : MonoBehaviour
{
    [SerializeField] private Image _panel;
    [SerializeField] private Teleportator _teleportator;
    [SerializeField] private WaterLevelRise _waterLevelRise;

    private float _speed = 1f;
    private WaitForFixedUpdate _waitForFixedUpdate;
    private Coroutine _coroutine;

    private void Awake()
    {
        _waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Water water))
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(nameof(Drown));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Water water))
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(nameof(Breath));
        }
    }

    private IEnumerator Drown()
    {
        while (_panel.color.a < 1f)
        {
            _panel.color = new Color(0, 0, 0, _panel.color.a + 0.1f * _speed * Time.deltaTime);

            yield return _waitForFixedUpdate;
        }

        Dead();
    }

    private void Dead()
    {
        _panel.color = new Color(0, 0, 0, 0);
        _waterLevelRise.SetDefaultLevel();
        _coroutine = null;
        _teleportator.Teleport();
    }

    private IEnumerator Breath()
    {
        while (_panel.color.a > 0f)
        {
            _panel.color = new Color(0, 0, 0, _panel.color.a - 0.1f * _speed * 20f * Time.deltaTime);

            yield return _waitForFixedUpdate;
        }
    }
}
