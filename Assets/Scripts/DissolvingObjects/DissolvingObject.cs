using DG.Tweening;
using System.Linq;
using UnityEngine;

public class DissolvingObject : MonoBehaviour
{
    private const float DissolveTolerance = 0.01f;
    private const float FlashAnimationDuration = 0.1f;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _originalMaterial;

    private Material _material;

    private float _revealingStartDistance = 3f;
    private bool _revealed = false;

    private void Awake()
    {
        _material = _meshRenderer.material;
    }

    private void Update()
    {
        if (_revealed == false)
            UpdateDissolve();
    }

    private void UpdateDissolve()
    {
        float dissolveAmount = 1f -
            Mathf.Max(0f,
            _revealingStartDistance * _revealingStartDistance - (Player.Position - transform.position).sqrMagnitude + 2f) /
            (_revealingStartDistance * _revealingStartDistance);

        if (dissolveAmount < DissolveTolerance)
        {
            _revealed = true;

            _material.SetFloat("_Reveal", 0f);
            _material.SetFloat("_EdgeWidth", 0f);

            Flash();
        }
        else
        {
            _material.SetFloat("_Reveal", dissolveAmount);
            _material.SetFloat("_EdgeWidth", dissolveAmount);
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
