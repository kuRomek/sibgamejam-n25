using System.Collections;
using UnityEngine;

public class DoorInteractor : MonoBehaviour
{
    [SerializeField] private Teleportator _teleportator;
    [SerializeField] private Transform _panel;
    [SerializeField] private Transform _flashlight;
    [SerializeField] private Transform _overheating;

    public void BecomeInteractable(bool hasCome)
    {
        if (hasCome)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(nameof(Flashback));
            }
        }
    }

    private IEnumerator Flashback()
    {
        _flashlight.gameObject.SetActive(false);
        _overheating.gameObject.SetActive(false);
        _panel.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        _teleportator.Teleport();

        _flashlight.gameObject.SetActive(true);
        _overheating.gameObject.SetActive(true);
        _panel.gameObject.SetActive(false);
    }
}