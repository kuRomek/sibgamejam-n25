using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private DoorInteractor _doorInteractor; 
    [SerializeField] private Transform _door;
    [SerializeField] private float _distance;

    private void Update() =>
        _doorInteractor.BecomeInteractable(Vector3.Distance(_door.position, _player.transform.position) < _distance);
}