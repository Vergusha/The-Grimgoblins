using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using SaveManagerNamespace;

public class MainMenu : MonoBehaviour
{
    public GameObject savesPanel;
    public TMP_Text[] infoTexts; // 0 - slot1, 1 - slot2, 2 - slot3

    private void Start()
    {
        if (savesPanel != null)
            savesPanel.SetActive(false);
        UpdateAllInfoTexts();
    }

    // Одна кнопка Play вызывает этот метод
    public void OnPlayButton()
    {
        if (savesPanel != null)
            savesPanel.SetActive(true);
        UpdateAllInfoTexts();
    }

    public void ExitSavesPanel()
    {
        if (savesPanel != null)
            savesPanel.SetActive(false);
    }

    // Вызывается из кнопки Play в каждом слоте
    public void OnSlotPlay(int slot)
    {
        var save = SaveManagerNamespace.SaveManager.Instance.GetSave(slot);
        if (save == null || save.playTime <= 0)
        {
            // Нет сохранения — создаём новое
            SaveManagerNamespace.SaveManager.Instance.NewGame(slot);
        }
        else
        {
            // Есть сохранение — просто загружаем
            SaveManagerNamespace.SaveManager.Instance.LoadGame(slot);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestScene");
    }

    // Вызывается из кнопки удалить в каждом слоте
    public void OnSlotDelete(int slot)
    {
        var save = SaveManagerNamespace.SaveManager.Instance.GetSave(slot);
        if (save != null && save.playTime > 0)
        {
            SaveManagerNamespace.SaveManager.Instance.DeleteSave(slot);
            UpdateInfoText(slot);
        }
    }

    private void UpdateAllInfoTexts()
    {
        for (int i = 0; i < infoTexts.Length; i++)
            UpdateInfoText(i);
    }

    private void UpdateInfoText(int slot)
    {
        var save = SaveManagerNamespace.SaveManager.Instance.GetSave(slot);
        if (infoTexts == null || infoTexts.Length <= slot) return;
        if (save == null || save.playTime <= 0)
        {
            infoTexts[slot].text = "ПУСТО";
        }
        else
        {
            System.TimeSpan t = System.TimeSpan.FromSeconds(save.playTime);
            infoTexts[slot].text = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
        }
    }
}
