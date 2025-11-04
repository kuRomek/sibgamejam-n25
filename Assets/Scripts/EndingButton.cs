using UnityEngine;

public class EndingButton : MonoBehaviour
{
    [SerializeField] private RectTransform _endingMenu;
    [SerializeField] private RectTransform _ending;

    public void OnButtonClick()
    {
        _endingMenu.gameObject.SetActive(false);
        _ending.gameObject.SetActive(true);
    }
}