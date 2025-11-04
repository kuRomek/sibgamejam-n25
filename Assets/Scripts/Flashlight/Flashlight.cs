using System;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private MeshRenderer _beamMeshRenderer;
    [SerializeField] private float _maxOverheatTime;
    [SerializeField] private float _overheatCooldown;
    [SerializeField] private Collider _beamCollider;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickSound;

    private Material _material;

    public event Action Overheating;

    public static Collider Collider { get; private set; }
    public float AccumulatedOverheat { get; private set; }
    public bool Shooting { get; private set; }
    public bool Overheated { get; private set; }
    public float OverheatCooldown => _overheatCooldown;
    public float MaxOverheatTime => _maxOverheatTime;

    private void Awake()
    {
        _material = _beamMeshRenderer.material;
        Collider = _beamCollider;
    }

    private void Update()
    {
        Shooting = Input.GetMouseButton(0);

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            _audioSource.PlayOneShot(_clickSound);

        if (Overheated == false)
        {
            if (Shooting)
            {
                _beamMeshRenderer.gameObject.SetActive(true);
                Shoot();
            }
            else
            {
                _beamMeshRenderer.gameObject.SetActive(false);
                Cooling();
            }
        }
        else
        {
            _beamMeshRenderer.gameObject.SetActive(false);
            Cooling();
        }

        Overheating?.Invoke();
    }

    private void Cooling()
    {
        AccumulatedOverheat = Mathf.Max(0f, AccumulatedOverheat - Time.deltaTime);

        if (AccumulatedOverheat == 0f)
            Overheated = false;
    }

    private void Shoot()
    {
        float offset = Mathf.Repeat((_material.mainTextureOffset + Vector2.down * Time.deltaTime).y, 1f);

        _material.mainTextureOffset = new Vector2(0, offset);

        AccumulatedOverheat = Mathf.Min(_maxOverheatTime, AccumulatedOverheat + Time.deltaTime);

        if (AccumulatedOverheat == _maxOverheatTime)
        {
            AccumulatedOverheat = _overheatCooldown;
            Overheated = true;
            Shooting = false;
        }
    }
}
