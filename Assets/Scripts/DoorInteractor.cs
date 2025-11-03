using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteractor : MonoBehaviour
{
    [SerializeField] private Teleportator _teleportator;
    [SerializeField] private Image _panel;
    [SerializeField] private Transform _flashlight;
    [SerializeField] private Transform _overheating;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _speed;

    public void BecomeInteractable(bool hasCome)
    {
        if (hasCome)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(nameof(Flashback));
            }
        }
    }

    private IEnumerator Flashback()
    {
        _flashlight.gameObject.SetActive(false);
        _overheating.gameObject.SetActive(false);

        while (_panel.color.a == 1f)
        {
            _panel.color = new Color(0, 0, 0, _panel.color.a + 0.1f * _speed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        _audioSource.PlayOneShot(_audioClip);

        yield return new WaitWhile(() => _audioSource.isPlaying);

        _teleportator.Teleport();

        _flashlight.gameObject.SetActive(true);
        _overheating.gameObject.SetActive(true);

        while (_panel.color.a == 0f)
        {
            _panel.color = new Color(0, 0, 0, _panel.color.a - 0.1f * _speed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
    }
}