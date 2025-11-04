using UnityEngine;

public class Finalizer : MonoBehaviour
{
    [SerializeField] private RectTransform _ending;

    public void ShowEnding() =>
        _ending.gameObject.SetActive(true);
}