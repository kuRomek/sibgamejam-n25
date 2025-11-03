using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioKeeper _audioKeeper;
    [SerializeField] private DissolvingObject _dissolvingObject;

    public void OnEnable() =>
        _dissolvingObject.Revealed += PlayClip;

    private void OnDisable() =>
        _dissolvingObject.Revealed -= PlayClip;

    public void PlayClip()
    {
        int randomValue = Random.Range(0, _audioKeeper.AudioClips.Count);

        _audioSource.PlayOneShot(_audioKeeper.AudioClips[randomValue]);
    }
}
