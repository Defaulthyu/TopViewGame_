using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;

    public int currentGold = 0;
    Text goldText;
    public Text goldText2;
    string savePath;

    [System.Serializable]
    class SaveData
    {
        public int gold;
    }

    private void Awake()
    {
        goldText = GetComponent<Text>();
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


    public delegate void GoldChangedHandler(int newGold);
    public static event GoldChangedHandler OnGoldChanged;

    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
        SaveGoldToJSON();

        OnGoldChanged?.Invoke(currentGold); // 골드 변경 이벤트 호출
    }

    void UpdateGoldUI()
    {
        if (goldText != null)
            goldText.text = $"{currentGold}";
        if (goldText2 != null)
            goldText2.text = $"{currentGold}";
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
