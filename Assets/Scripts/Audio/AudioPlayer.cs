using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(DissolvingObject))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioKeeper _audioKeeper;

    private DissolvingObject _dissolvingObject;
    private AudioSource _audioSource;

    public void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _dissolvingObject = GetComponent<DissolvingObject>();
        _dissolvingObject.Revealed += PlayClip;
    }

    private void OnDisable() =>
        _dissolvingObject.Revealed -= PlayClip;

    public void PlayClip()
    {
        int randomValue = Random.Range(0, _audioKeeper.AudioClips.Count);

        _audioSource.PlayOneShot(_audioKeeper.AudioClips[randomValue]);
    }
}
