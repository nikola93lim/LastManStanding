using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private SwipeController _swipeController;

    private void Start()
    {
        _levelManager.OnLevelComplete += LevelManager_OnLevelComplete;
        _levelManager.OnLevelStart += LevelManager_OnLevelStart;

        _playerController = FindObjectOfType<PlayerController>();
        _swipeController = FindObjectOfType<SwipeController>();
    }

    private void LevelManager_OnLevelStart()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _swipeController = FindObjectOfType<SwipeController>();

        EnableObjects(true);
    }

    private void LevelManager_OnLevelComplete()
    {
        EnableObjects(false);
    }

    private void EnableObjects(bool b)
    {
        _playerController.enabled = b;
        _playerInput.enabled = b;
        _swipeController.enabled = b;
    }
}
