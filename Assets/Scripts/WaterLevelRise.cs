using System.Collections;
using UnityEngine;

public class WaterLevelRise : MonoBehaviour
{
   [SerializeField] private float _targetWaterLevelScaleY = 8f;
   [SerializeField] private float _duration = 8f;

    private Vector3 initialScale;
    private bool hasRisen = false;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    public void ActivateWaterRise()
    {
        if (hasRisen == false) 
        {
            StartCoroutine(RiseWaterCoroutine());
            hasRisen = true; 
        }
    }

    private IEnumerator RiseWaterCoroutine()
    {
        float elapsedTime = 0f; 

        if (initialScale == Vector3.zero)
            initialScale = transform.localScale;

        while (elapsedTime < _duration)
        {
            float progress = elapsedTime / _duration;
            float currentScaleY = Mathf.Lerp(initialScale.y, _targetWaterLevelScaleY, progress);
            transform.localScale = new Vector3(initialScale.x, currentScaleY, initialScale.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localScale = new Vector3(initialScale.x, _targetWaterLevelScaleY, initialScale.z);
    }
}