using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private WaterLevelRise _waterLevelRise;

    private bool _hasWaterFillStarted = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _) == false)
            return;

        if (_hasWaterFillStarted)
            return;

        _waterLevelRise.ActivateWaterRise();
        _hasWaterFillStarted = true;
    }
}