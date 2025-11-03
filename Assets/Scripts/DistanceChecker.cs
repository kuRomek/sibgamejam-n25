using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    [SerializeField] private DoorInteractor _doorInteractor; 
    [SerializeField] private Transform _door;
    [SerializeField] private float _distance;

    private void Update() =>
        _doorInteractor.BecomeInteractable(Vector3.Distance(_door.position, Player.Position) < _distance);
}