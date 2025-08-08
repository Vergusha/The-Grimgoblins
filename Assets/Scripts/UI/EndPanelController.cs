using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using SaveManagerNamespace;

public class EndPanelController : MonoBehaviour
{
    public int currentSaveSlot = 0; // Установи номер слота при запуске игры
    public GameObject panel;
    public TMP_Text killText;
    public TMP_Text timeText;
    public KillCounter killCounter;
    public GameTimer gameTimer;

    public void PauseGame()
    {
        ShowPanel();
        Time.timeScale = 0f;
    }

    private void Start()
    {
        panel.SetActive(false);
    }

    public void ShowPanel()
    {
        if (panel != null)
            panel.SetActive(true);
        if (killText != null && killCounter != null)
            killText.text = killCounter.GetKillCount().ToString();
        float elapsed = 0f;
        if (gameTimer != null)
            elapsed = gameTimer.GetElapsedTime();
        if (timeText != null)
        {
            int minutes = Mathf.FloorToInt(elapsed / 60f);
            int seconds = Mathf.FloorToInt(elapsed % 60f);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    // Сохраняем время в выбранный слот
    SaveManager.Instance.SavePlayTime(currentSaveSlot, elapsed);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
