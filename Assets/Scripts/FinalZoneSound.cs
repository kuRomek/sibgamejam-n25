using System.Collections;
using UnityEngine;

public class FinalZoneSound : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;  
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _delay = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player _))
            StartCoroutine(PlaySoundAfterDelay(_delay));
    }

    private IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_audioSource != null && _audioClip != null)
            _audioSource.PlayOneShot(_audioClip);
    }
}