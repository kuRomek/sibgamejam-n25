using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteractor : MonoBehaviour
{
    [SerializeField] private Teleportator _teleportator;

    public void BecomeInteractable(bool hasCome)
    {
        if (hasCome)             
            if (Input.GetKeyDown(KeyCode.E))
                _teleportator.Teleport();
    }    
}
