using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action<Vector2> OnSwipe;

    private Vector2 touchStartPos;
    private float swipeThreshold = 50f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 tmp = Input.mousePosition;
            Vector2 swipeDelta = tmp - touchStartPos;

            if (swipeDelta.magnitude > swipeThreshold)
            {
                OnSwipe?.Invoke(swipeDelta.normalized);
            }
        }
    }
}
