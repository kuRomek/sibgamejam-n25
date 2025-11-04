using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class DoorInteractor : MonoBehaviour
{
    [SerializeField] private Teleportator _teleportator;
    [SerializeField] private Finalizer _finalizer;
    [SerializeField] private Image _panel;
    [SerializeField] private Transform _flashlight;
    [SerializeField] private Transform _overheating;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _isOpen = true;

    private float _speed = 10f;
    private AudioSource _audioSource;
    private WaitForFixedUpdate _waitForFixedUpdate;

    private void Awake()
    {
        _waitForFixedUpdate = new WaitForFixedUpdate();
        _audioSource = GetComponent<AudioSource>();
    }

    public void BecomeInteractable(bool hasCome)
    {
        if (hasCome)
        {
            if (Input.GetKeyDown(KeyCode.E) && _isOpen == true)
            {
                if (_teleportator != null)
                {
                    Player.ToggleInvisible(true);
                    _teleportator.Teleport();
                    StartCoroutine(nameof(Flashback));
                }

                if(_finalizer != null)
                    _finalizer.ShowEnding();
            }
        }
    }

    public void Open() =>
        _isOpen = true;

    private IEnumerator Flashback()
    {

        _flashlight.gameObject.SetActive(false);
        _overheating.gameObject.SetActive(false);

        while (_panel.color.a < 1f)
        {
            _panel.color = new Color(0, 0, 0, _panel.color.a + 0.1f * _speed * Time.deltaTime);

            yield return _waitForFixedUpdate;

        }

        _audioSource.PlayOneShot(_audioClip);

        yield return new WaitWhile(() => _audioSource.isPlaying);

        _flashlight.gameObject.SetActive(true);
        _overheating.gameObject.SetActive(true);

        while (_panel.color.a > 0f)
        {
            _panel.color = new Color(0, 0, 0, _panel.color.a - 0.1f * _speed * Time.deltaTime);

            yield return _waitForFixedUpdate;
        }

        Player.ToggleInvisible(false);
    }
}