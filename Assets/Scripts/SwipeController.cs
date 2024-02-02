using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float rotationAmount = 20f;
    private bool canSwipe = true;

    private void OnEnable()
    {
        PlayerInput.OnSwipe += HandleSwipe;
    }

    private void OnDisable()
    {
        PlayerInput.OnSwipe -= HandleSwipe;
    }

    void HandleSwipe(Vector2 swipeDirection)
    {
        // Check if the swipe is more horizontal or vertical
        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        {
            // Horizontal swipe
            if (swipeDirection.x > 0)
            {
                // Swiped right
                RotateObject(Vector3.forward * -rotationAmount);
            }
            else
            {
                // Swiped left
                RotateObject(Vector3.forward * rotationAmount);
            }
        }
        else
        {
            // Vertical swipe
            if (swipeDirection.y > 0)
            {
                // Swiped up
                RotateObject(Vector3.right * rotationAmount);
            }
            else
            {
                // Swiped down
                RotateObject(Vector3.right * -rotationAmount);
            }
        }
    }

    void RotateObject(Vector3 rotationVector)
    {
        if (!canSwipe) return;

        canSwipe = false;

        transform.DORotate(rotationVector, 0.5f, RotateMode.Fast)
            .OnComplete(ResetRotation); ;
    }

    void ResetRotation()
    {
        transform.DORotate(Vector3.zero, 0.25f, RotateMode.Fast)
            .OnComplete(() => canSwipe = true);
    }
}
