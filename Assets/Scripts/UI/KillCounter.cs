using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public TMP_Text killText;
    private int killCount = 0;

    private void Start()
    {
        UpdateKillText();
    }

    public void AddKill()
    {
        killCount++;
        UpdateKillText();
    }

    private void UpdateKillText()
    {
        if (killText != null)
            killText.text = killCount.ToString();
    }

    public int GetKillCount()
    {
        return killCount;
    }
}
