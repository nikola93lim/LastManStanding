using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedUI : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private GameObject _levelCompleteUI;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _nextLevelButton;

    private void Start()
    {
        _levelManager.OnLevelComplete += LevelManager_OnLevelComplete;
        _homeButton.onClick.AddListener(Home);
        _nextLevelButton.onClick.AddListener(NextLevel);
    }

    private void NextLevel()
    {
        _levelCompleteUI.SetActive(false);
        _levelManager.StartNextLevel();
    }

    private void Home()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    private void LevelManager_OnLevelComplete()
    {
        _levelCompleteUI.SetActive(true);
    }
}
