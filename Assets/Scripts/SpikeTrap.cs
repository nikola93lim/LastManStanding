using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private const string ACTIVATE = "Activate";
    [SerializeField] private LayerMask _tileLayerMask;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        AssignToTile();
    }

    private void AssignToTile()
    {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, Mathf.Infinity, _tileLayerMask))
        {
            hit.transform.GetComponent<Tile>().SetSpikeTrap(this);
        }
    }

    public void Activate()
    {
        _animator.SetTrigger(ACTIVATE);
    }
}
