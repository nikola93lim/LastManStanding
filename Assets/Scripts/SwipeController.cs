using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public event Action<Vector3> OnSwipe;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float rotationAmount = 20f;
    private bool canSwipe = true;

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
        if (!canSwipe) return;

        // Check if the swipe is more horizontal or vertical
        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        {
            // Horizontal swipe
            if (swipeDirection.x > 0)
            {
                // Swiped right
                OnSwipe?.Invoke(Vector3.right);
                RotateObject(Vector3.forward * -rotationAmount);
            }
            else
            {
                // Swiped left
                OnSwipe?.Invoke(Vector3.left);
                RotateObject(Vector3.forward * rotationAmount);
            }
        }
        else
        {
            // Vertical swipe
            if (swipeDirection.y > 0)
            {
                // Swiped up
                OnSwipe?.Invoke(Vector3.forward);
                RotateObject(Vector3.right * rotationAmount);
            }
            else
            {
                // Swiped down
                OnSwipe?.Invoke(Vector3.back);
                RotateObject(Vector3.right * -rotationAmount);
            }
        }
    }

    void RotateObject(Vector3 rotationVector)
    {
        canSwipe = false;

        transform.DORotate(rotationVector, 0.5f, RotateMode.Fast);
    }

    void ResetRotation()
    {
        transform.DORotate(Vector3.zero, 0.25f, RotateMode.Fast)
            .OnComplete(() => canSwipe = true);
    }
}
