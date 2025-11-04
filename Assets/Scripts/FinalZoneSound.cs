using UnityEngine;

public class FinalZoneSound : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    
    private AudioSource _audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player _))
            _audioSource.PlayOneShot(_audioClip);
    }
}