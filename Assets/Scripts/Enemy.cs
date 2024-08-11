using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> OnDeath;
    [SerializeField] private LayerMask _tileLayerMask;

    private void Start()
    {
        AssignToTile();
    }
    public void Die()
    {
        Destroy(gameObject);
        OnDeath?.Invoke(this);
    }

    private void AssignToTile()
    {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, Mathf.Infinity, _tileLayerMask))
        {
            hit.transform.GetComponent<Tile>().SetEnemy(this);
        }
    }
}
