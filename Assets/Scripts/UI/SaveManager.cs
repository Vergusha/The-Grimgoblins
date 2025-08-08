using UnityEngine;
using System.IO;
using System;

namespace SaveManagerNamespace
{
    [Serializable]
    public class SaveData
    {
        public float playTime;
    }

    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get; private set; }
        private const int slotCount = 3;
        private SaveData[] saves = new SaveData[slotCount];
        private string[] savePaths;
        private int currentSlot = 0;

        public void SetCurrentSlot(int slot)
        {
            currentSlot = slot;
        }

        public int GetCurrentSlot() => currentSlot;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            savePaths = new string[slotCount];
            for (int i = 0; i < slotCount; i++)
                savePaths[i] = Application.persistentDataPath + $"/save_slot_{i+1}.json";
            LoadAllSaves();
        }

        public void NewGame(int slot)
        {
            saves[slot] = new SaveData { playTime = 0 };
            SaveToFile(slot);
            SetCurrentSlot(slot);
        }

        public void LoadGame(int slot)
        {
            LoadFromFile(slot);
            SetCurrentSlot(slot);
        }

    public void DeleteSave(int slot)
    {
        saves[slot] = null;
        if (File.Exists(savePaths[slot]))
            File.Delete(savePaths[slot]);
    }

        public void SavePlayTime(float playTime)
        {
            int slot = currentSlot;
            if (saves[slot] == null)
                saves[slot] = new SaveData();
            saves[slot].playTime = playTime;
            SaveToFile(slot);
        }

    public SaveData GetSave(int slot)
    {
        if (slot < 0 || slot >= slotCount) return null;
        return saves[slot];
    }

    public int SlotCount => slotCount;

    private void SaveToFile(int slot)
    {
        string json = JsonUtility.ToJson(saves[slot]);
        File.WriteAllText(savePaths[slot], json);
    }

    private void LoadFromFile(int slot)
    {
        if (File.Exists(savePaths[slot]))
        {
            string json = File.ReadAllText(savePaths[slot]);
            saves[slot] = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            saves[slot] = new SaveData();
        }
    }

    private void LoadAllSaves()
    {
        for (int i = 0; i < slotCount; i++)
            LoadFromFile(i);
    }
}
}
