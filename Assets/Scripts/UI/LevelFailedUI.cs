using UnityEngine;
using UnityEngine.UI;

public class LevelFailedUI : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private GameObject _levelFailedUI;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _tryAgainButton;

    private void Start()
    {
        _levelManager.OnLevelFailed += LevelManager_OnLevelFailed;
        _homeButton.onClick.AddListener(Home);
        _tryAgainButton.onClick.AddListener(TryAgain);
    }

    private void TryAgain()
    {
        _levelFailedUI.SetActive(false);
        _levelManager.StartNextLevel();
    }

    private void Home()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    private void LevelManager_OnLevelFailed()
    {
        _levelFailedUI.SetActive(true);
    }
}
