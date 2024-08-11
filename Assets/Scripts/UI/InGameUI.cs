using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private TextMeshProUGUI _enemyCountText;
    [SerializeField] private Image _enemyCountBar;
    [SerializeField] private TextMeshProUGUI _levelText;

    private Level _currentLevel;

    private void Start()
    {
        _currentLevel = _levelManager.GetCurrentLevel();
        _currentLevel.OnEnemyDied += UpdateEnemyCountText;

        _levelManager.OnLevelComplete += LevelManager_OnLevelComplete;
        _levelManager.OnLevelStart += LevelManager_OnLevelStart;

        UpdateEnemyCountText();
        UpdateLevelText();
    }

    private void LevelManager_OnLevelStart()
    {
        // update current level and subscribe to it's events
        _currentLevel = _levelManager.GetCurrentLevel();
        _currentLevel.OnEnemyDied += UpdateEnemyCountText;

        UpdateEnemyCountText();
        UpdateLevelText();
    }

    private void LevelManager_OnLevelComplete()
    {
        // unsubscribe from previous level events
        _currentLevel.OnEnemyDied -= UpdateEnemyCountText;
    }

    private void UpdateEnemyCountText()
    {
        // update ui text
        int totalNumberOfEnemiesInTheLevel = _currentLevel.GetTotalNumberOfEnemies();
        int currentNumberOfEnemiesAlive =_currentLevel.GetCurrentNumberOfEnemiesAlive();

        _enemyCountText.text = $"{totalNumberOfEnemiesInTheLevel - currentNumberOfEnemiesAlive} / {totalNumberOfEnemiesInTheLevel}";
        _enemyCountBar.fillAmount = 1 - (float) currentNumberOfEnemiesAlive / totalNumberOfEnemiesInTheLevel;
    }

    private void UpdateLevelText()
    {
        _levelText.text = $"Level {_levelManager.GetCurrentLevelIndex() + 1}";
    }
}
