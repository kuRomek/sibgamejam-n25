using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;

public class DissolvingObject : MonoBehaviour
{
    private const float DissolveTolerance = 0.01f;
    private const float FlashAnimationDuration = 0.1f;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _originalMaterial;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _revealingSpeed;

    private Material _material;

    private float _revealingStartDistance = 3f;
    private float _revealingStartDistanceBeamSquared = 1f;
    private bool _revealed = false;
    private float _dissolveAmount;

    public event Action Revealed;

    private void OnValidate()
    {
        if (_meshRenderer == null)
            _meshRenderer = GetComponent<MeshRenderer>();

        if (_collider == null)
            _collider = GetComponent<Collider>();
    }

    private void Awake()
    {
        _material = _meshRenderer.material;
    }

    private void Start()
    {
        _material.SetFloat("_Reveal", 1f);
        _material.SetFloat("_EdgeWidth", 1f);
    }

    private void Update()
    {
        if (_revealed == false)
            UpdateDissolve();
    }

    private void UpdateDissolve()
    {
        float playerDistance = DistanceCalculator.Distance(_collider, Player.Collider);
        float beamDistance = 9999999f;

        if (Flashlight.Collider.gameObject.activeSelf == true)
        {
            Vector3 beamClosestPoint = new Vector3(-10000, -10000, -10000);

            beamClosestPoint = Flashlight.Collider.ClosestPoint(transform.position);
            beamDistance = (beamClosestPoint - transform.position).sqrMagnitude;
        }

        if (playerDistance < beamDistance)
        {
            _dissolveAmount = 1f -
                Mathf.Max(0f, _revealingStartDistance - playerDistance) / _revealingStartDistance;
        }
        else if (beamDistance < _revealingStartDistanceBeamSquared)
        {
            _dissolveAmount = Mathf.Max(0f, _dissolveAmount - _revealingSpeed * Time.deltaTime);
        }

        if (_dissolveAmount < DissolveTolerance)
        {
            _revealed = true;

            _material.SetFloat("_Reveal", 0f);
            _material.SetFloat("_EdgeWidth", 0f);

            Flash();
        }
        else
        {
            _material.SetFloat("_Reveal", _dissolveAmount);
            _material.SetFloat("_EdgeWidth", _dissolveAmount);
        }
    }

    private void Flash()
    {
        DOTween.Sequence().
            Append(DOVirtual.Color(_material.GetColor("_MainColor"), Color.white, FlashAnimationDuration,
                (value) => _material.SetColor("_MainColor", value)).OnComplete(() =>
                {
                    var materials = _meshRenderer.materials.ToList();
                    materials.Add(_originalMaterial);
                    _meshRenderer.materials = materials.ToArray();
                })).
            Append(DOVirtual.Color(Color.white, new Color(1f, 1f, 1f, 0f), FlashAnimationDuration,
                (value) => _material.SetColor("_MainColor", value)).OnComplete(() =>
                {
                    var materials = _meshRenderer.materials.ToList();
                    materials.RemoveAt(0);
                    _meshRenderer.materials = materials.ToArray();
                }));
    }
}
