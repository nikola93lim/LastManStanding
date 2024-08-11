using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnFinishedMovement;

    [SerializeField] private LayerMask _tileLayerMask;
    [SerializeField] private LayerMask _fieldLayerMask;

    [SerializeField] private Transform _upperRaycaster;
    [SerializeField] private Transform _lowerRaycaster;

    [SerializeField] private float _moveSpeed = 1f;

    private SwipeController _swipeController;
    private bool _isMoving;
    private WaitForSeconds _delayTime = new WaitForSeconds(0.2f);

    private Tile _targetTile;

    private void Start()
    {
        _swipeController = FindObjectOfType<SwipeController>();
        _swipeController.OnSwipe += SwipeController_OnSwipe;
    }

    private void Update()
    {
        if (!_isMoving) return;
        StickToGround();
    }

    private void SwipeController_OnSwipe(Vector3 direction)
    {
        RaycastHit[] hits = Physics.RaycastAll(_lowerRaycaster.position, direction.normalized, Mathf.Infinity, _tileLayerMask);

        if (hits.Length == 0)
        {
            StartCoroutine(Delay());
            return;
        }

        _isMoving = true;
        Array.Sort(hits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.GetComponent<Tile>().HasObstacle())
            {
                if (i == 0)
                {
                    StartCoroutine(Delay());
                    return;
                }

                _targetTile = hits[i - 1].transform.GetComponent<Tile>();
                break;
            }

            if (hits[i].transform.GetComponent<Tile>().TryGetEnemy(out Enemy enemy))
            {
                _targetTile = hits[i].transform.GetComponent<Tile>();
                break;
            }
        }

        if (_targetTile == null)
        {
            transform.DOLocalMove(hits[hits.Length - 1].transform.GetComponent<Tile>().GetTargetPosition(), _moveSpeed)
            .SetSpeedBased(true)
            .SetEase(Ease.InCubic)
            .OnComplete(FinishMovement);
        }
        else
        {
            transform.DOLocalMove(_targetTile.GetTargetPosition(), _moveSpeed)
            .SetSpeedBased(true)
            .SetEase(Ease.InCubic)
            .OnComplete(FinishMovement);
        }
        
    }

    private IEnumerator Delay()
    {
        yield return _delayTime;
        FinishMovement();
    }

    private void FinishMovement()
    {
        if (_targetTile != null)
        {
            _targetTile.ClearTile();
            _targetTile = null;
        }

        _isMoving = false;
        OnFinishedMovement?.Invoke();
    }

    private void StickToGround()
    {
        // Adjust the player's position to stick to the ground
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, Mathf.Infinity, _fieldLayerMask))
        {
            transform.DOMoveY(hit.point.y, 0.001f);
        }
    }
}
