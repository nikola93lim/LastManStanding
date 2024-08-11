using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _currentLevel = 0;

    private Level[] _levels;

    private void Start()
    {
        _levels = GetComponentsInChildren<Level>(true);
        _levels[_currentLevel].OnLevelComplete += CurrentLevel_OnLevelComplete;
    }

    private void CurrentLevel_OnLevelComplete()
    {
        Debug.Log("Changing level");
        ChangeLevels(_currentLevel, ++_currentLevel);
        
    }

    private void ChangeLevels(int currentLevel, int nextLevel)
    {
        _levels[currentLevel].OnLevelComplete -= CurrentLevel_OnLevelComplete;
        _levels[currentLevel].gameObject.SetActive(false);

        _levels[nextLevel].gameObject.SetActive(true);
        _levels[nextLevel].OnLevelComplete += CurrentLevel_OnLevelComplete;
    }
}
