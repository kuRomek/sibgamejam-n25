using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private DoorInteractor _doorInteractor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
        {
            _doorInteractor.Open();
            Destroy(gameObject);
        }    
    }
}