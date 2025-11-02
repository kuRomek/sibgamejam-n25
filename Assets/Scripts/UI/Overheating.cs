using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Overheating : MonoBehaviour
{
    [SerializeField] private Flashlight _flashlight;
    [SerializeField] private Image _filledImage;
    [SerializeField] private RectTransform _rectTransform;

    private Tween _shaking;
    private TweenCallback _shakeTweenComplete;
    private float _shakeStrength;

    private void Start()
    {
        _filledImage.fillAmount = 0f;

        _shakeTweenComplete = () =>
        {
            if (_shakeStrength > 0f)
                _shaking = _rectTransform.DOShakeAnchorPos(0.1f, strength: 15f * _shakeStrength, vibrato: 70)
                    .OnComplete(_shakeTweenComplete);
            else
                DOVirtual.DelayedCall(0.1f, _shakeTweenComplete);
        };

        _shakeTweenComplete.Invoke();
    }

    private void OnEnable()
    {
        _flashlight.Overheating += UpdateView;
    }

    private void OnDisable()
    {
        _shaking?.Kill();
        _flashlight.Overheating -= UpdateView;
    }

    private void UpdateView()
    {
        if (_flashlight.Overheated)
        {
            _filledImage.fillAmount = _flashlight.AccumulatedOverheat / _flashlight.OverheatCooldown;
            _shakeStrength = 0f;
        }
        else
        {
            _filledImage.fillAmount = _flashlight.AccumulatedOverheat / _flashlight.MaxOverheatTime;
            _shakeStrength = _filledImage.fillAmount;
        }
    }
}
