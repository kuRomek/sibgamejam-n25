using System.Collections.Generic;
using UnityEngine;

public class AudioKeeper : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips;
    
    public IReadOnlyList<AudioClip> AudioClips => _audioClips;
}
