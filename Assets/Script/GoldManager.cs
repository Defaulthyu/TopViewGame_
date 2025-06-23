using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;

    public int currentGold = 0;
    public Text goldText; // UI에 표시될 텍스트 연결
    public Text goldText2;
    string savePath;

    [System.Serializable]
    class SaveData
    {
        public int gold;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Application.persistentDataPath + "/playerdata.json";
            LoadGold();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
        SaveGoldToJSON();
    }

    void UpdateGoldUI()
    {
        if (goldText != null)
            goldText.text = $"{currentGold}";
        if (goldText2 != null)
            goldText2.text = $"{currentGold}"; // 두 번째 텍스트도 업데이트
    }

    void SaveGoldToJSON()
    {
        SaveData data = new SaveData();
        data.gold = currentGold;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    void LoadGold()
    {
        savePath = Application.persistentDataPath + "/playerdata.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            currentGold = data.gold;
        }

        UpdateGoldUI();
    }
}
