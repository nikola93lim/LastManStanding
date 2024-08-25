using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private TextMeshProUGUI _enemyCountText;
    [SerializeField] private Image _enemyCountBar;
    [SerializeField] private TextMeshProUGUI _levelText;

    private void Start()
    {
        _levelManager.OnEnemyDied += LevelManager_OnEnemyDied;
        UpdateEnemyCountText();
        UpdateLevelText();
    }

    private void LevelManager_OnEnemyDied()
    {
        UpdateEnemyCountText();
    }

    private void UpdateEnemyCountText()
    {
        // update ui text
        int totalNumberOfEnemiesInTheLevel = _levelManager.GetTotalNumberOfEnemies();
        int currentNumberOfEnemiesAlive = _levelManager.GetCurrentNumberOfEnemiesAlive();

        _enemyCountText.text = $"{totalNumberOfEnemiesInTheLevel - currentNumberOfEnemiesAlive} / {totalNumberOfEnemiesInTheLevel}";
        _enemyCountBar.fillAmount = 1 - (float) currentNumberOfEnemiesAlive / totalNumberOfEnemiesInTheLevel;
    }

    private void UpdateLevelText()
    {
        _levelText.text = $"Level {SceneManager.GetActiveScene().buildIndex.ToString()}";
    }
}
