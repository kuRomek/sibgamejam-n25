using UnityEngine;
using UnityEngine.UI;

public class DoorInteractor : MonoBehaviour
{
    [SerializeField] private Teleportator _teleportator;
    [SerializeField] private Image _sprite;

    public void BecomeInteractable(bool hasCome)
    {
        if (hasCome)
        {
            _sprite.gameObject.SetActive(true);                    

            if (Input.GetKeyDown(KeyCode.E))
            {
                _teleportator.Teleport();
            }
        }
        else
        {
            _sprite.gameObject.SetActive(false);
        }
    }    
}
