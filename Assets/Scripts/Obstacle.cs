using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private LayerMask _tileLayerMask;

    private void Start()
    {
        AssignToTile();
    }

    private void AssignToTile()
    {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, Mathf.Infinity, _tileLayerMask))
        {
            hit.transform.GetComponent<Tile>().SetObstacle(this);
        }
    }
}
