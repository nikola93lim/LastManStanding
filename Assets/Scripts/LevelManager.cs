using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public event Action OnLevelComplete;
    public event Action OnLevelStart;

    [SerializeField] private int _currentLevel = 0;
    private Level[] _levels;

    private void Awake()
    {
        _levels = GetComponentsInChildren<Level>(true);
        _levels[_currentLevel].OnLevelComplete += CurrentLevel_OnLevelComplete;
    }

    private void CurrentLevel_OnLevelComplete()
    {
        FinishLevel();
    }

    private void FinishLevel()
    {
        _levels[_currentLevel].OnLevelComplete -= CurrentLevel_OnLevelComplete;
        OnLevelComplete?.Invoke();
    }

    public void StartNextLevel()
    {
        _levels[_currentLevel].gameObject.SetActive(false);
        _currentLevel++;

        _levels[_currentLevel].gameObject.SetActive(true);
        _levels[_currentLevel].OnLevelComplete += CurrentLevel_OnLevelComplete;

        OnLevelStart?.Invoke();
    }

    public Level GetCurrentLevel() => _levels[_currentLevel];
}
