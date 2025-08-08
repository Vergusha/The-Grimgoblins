using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("TestScene");
        // Здесь можно добавить логику загрузки сохранения
    }

    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
