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

    [SerializeField] private Transform _field;
    [SerializeField] private Transform _upperRaycaster;
    [SerializeField] private Transform _lowerRaycaster;

    private SwipeController _swipeController;
    private bool _isMoving;
    private WaitForSeconds _delayTime = new WaitForSeconds(1f);

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
        transform.DOLocalMove(hits[hits.Length - 1].transform.GetComponent<Tile>().GetTargetPosition(), 1f)
            .SetEase(Ease.OutBounce)
            .OnComplete(FinishMovement);
    }

    private IEnumerator Delay()
    {
        yield return _delayTime;
        FinishMovement();
    }

    private void FinishMovement()
    {
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
