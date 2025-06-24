using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClickManager : MonoBehaviour
{
    public int goldCost = 50; // �������� �ʿ��� ��� ���
    public void OpenLevelUpByGold()
    {

        if (GoldManager.Instance.currentGold >= goldCost)
        {
            GoldManager.Instance.AddGold(-goldCost);
            GameManager.instance.uiLevelUp.Show();
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }


    Player_ player;
    public Button button; // UI ��ư
    public int cost = 50; //��Ȱ��ȭ �� �ڽ�Ʈ

    void Start()
    {
        if(player != null)
            player = GameManager.instance.player;

        if (GoldManager .Instance != null)
            UpdateButton(GoldManager.Instance.currentGold);
    }

    private void OnEnable()
    {
        GoldManager.OnGoldChanged += UpdateButton;
    }

    private void OnDisable()
    {
        GoldManager.OnGoldChanged -= UpdateButton;
    }

    void UpdateButton(int gold)
    {
        if (button != null)
            button.interactable = gold >= cost;
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Game");
    }

    public void Title()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title_Scene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}