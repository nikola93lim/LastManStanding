using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public event Action OnLevelComplete;
    public event Action OnLevelFailed;
    public event Action OnLevelStart;

    [SerializeField] private int _currentLevel = 0;
    private Level[] _levels;

    private void Awake()
    {
        _levels = GetComponentsInChildren<Level>(true);
        _levels[_currentLevel].OnLevelComplete += CurrentLevel_OnLevelComplete;
        _levels[_currentLevel].OnLevelFailed += CurrentLevel_OnLevelFailed;
    }

    private void CurrentLevel_OnLevelFailed()
    {
        FailLevel();
    }

    private void CurrentLevel_OnLevelComplete()
    {
        FinishLevel();
    }

    private void FailLevel()
    {
        _levels[_currentLevel].OnLevelFailed -= CurrentLevel_OnLevelFailed;
        OnLevelFailed?.Invoke();
    }

    private void FinishLevel()
    {
        _levels[_currentLevel].OnLevelComplete -= CurrentLevel_OnLevelComplete;
        OnLevelComplete?.Invoke();
    }

    public void RetryLevel()
    {

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
    public int GetCurrentLevelIndex() => _currentLevel;
}
