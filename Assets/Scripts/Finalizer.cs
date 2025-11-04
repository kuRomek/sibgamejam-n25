using UnityEngine;

public class Finalizer : MonoBehaviour
{
    [SerializeField] private RectTransform _endingMenu;

    public void ShowEnding()
    {
        _endingMenu.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}