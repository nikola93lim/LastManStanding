using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public event Action<Vector3> OnSwipe;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _rotationAmount = 20f;
    private bool _canSwipe = true;

    private Tween _tween;

    private void OnEnable()
    {
        PlayerInput.OnSwipe += HandleSwipe;
        _playerController.OnFinishedMovement += PlayerController_OnFinishedMovement;
    }

    private void OnDisable()
    {
        PlayerInput.OnSwipe -= HandleSwipe;
        _playerController.OnFinishedMovement += PlayerController_OnFinishedMovement;
    }

    private void PlayerController_OnFinishedMovement()
    {
        ResetRotation();
    }

    void HandleSwipe(Vector2 swipeDirection)
    {
        if (!_canSwipe) return;

        // Check if the swipe is more horizontal or vertical
        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        {
            // Horizontal swipe
            if (swipeDirection.x > 0)
            {
                // Swiped right
                OnSwipe?.Invoke(Vector3.right);
                RotateObject(Vector3.forward * -_rotationAmount);
            }
            else
            {
                // Swiped left
                OnSwipe?.Invoke(Vector3.left);
                RotateObject(Vector3.forward * _rotationAmount);
            }
        }
        else
        {
            // Vertical swipe
            if (swipeDirection.y > 0)
            {
                // Swiped up
                OnSwipe?.Invoke(Vector3.forward);
                RotateObject(Vector3.right * _rotationAmount);
            }
            else
            {
                // Swiped down
                OnSwipe?.Invoke(Vector3.back);
                RotateObject(Vector3.right * -_rotationAmount);
            }
        }
    }

    void RotateObject(Vector3 rotationVector)
    {
        _canSwipe = false;

        if (_tween != null && _tween.IsPlaying()) _tween.Kill();

        _tween = transform.DORotate(rotationVector, _rotationSpeed, RotateMode.Fast)
            .SetSpeedBased(true);
    }

    void ResetRotation()
    {
        if (_tween != null && _tween.IsPlaying()) _tween.Kill();

        _tween = transform.DORotate(Vector3.zero, _rotationSpeed, RotateMode.Fast)
            .SetSpeedBased(true)
            .OnComplete(() => _canSwipe = true);
    }
}
