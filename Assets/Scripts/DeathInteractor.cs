using UnityEngine;

public class DeathInteractor : MonoBehaviour
{
    [SerializeField] private Teleportator _teleportator;
    [SerializeField] private LayerMask _playerMask;

    private void Update()
    {
        if (Physics.BoxCast(transform.localPosition, transform.localScale, transform.localPosition, Quaternion.identity, 0f, _playerMask))
        {
            Debug.Log(1);
            _teleportator.Teleport();
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
