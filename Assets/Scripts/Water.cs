using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private WaterLevelRise _waterLevelRise;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player _)) 
        {
            Debug.Log("Игрок вошел в воду. Активирую подъем уровня воды.");
            _waterLevelRise.ActivateWaterRise();
        }
    }
}