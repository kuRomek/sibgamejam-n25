using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteractor : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _nextRoomPoint;
    [SerializeField] private Image _sprite;

    public void BecomeInteractable(bool hasCome)
    {
        if (hasCome)
        {
            _sprite.gameObject.SetActive(true);                    

            if (Input.GetKeyDown(KeyCode.E))
            {
                Teleport();
            }
        }
        else
        {
            _sprite.gameObject.SetActive(false);
        }
    }

    private void Teleport()
    {
        _characterController.enabled = false;
        _characterController.transform.position = _nextRoomPoint.position;
        _characterController.enabled = true;
    }
}
