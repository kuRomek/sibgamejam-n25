using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Teleportator _teleportator;

    private void FixedUpdate()
    {
        _navMeshAgent.SetDestination(Player.Position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player _))
            _teleportator.Teleport();
    }
}
