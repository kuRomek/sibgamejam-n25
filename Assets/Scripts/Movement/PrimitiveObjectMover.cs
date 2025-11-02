using UnityEngine;
using DG.Tweening;

public class PrimitiveObjectMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private LoopType _loopType = LoopType.Yoyo;
    [SerializeField] private float _duration;

    private void Start()
    {
        transform.DOMove(_target.position, _duration)
                 .SetLoops(-1, _loopType)
                 .SetEase(Ease.Linear);
    }
}