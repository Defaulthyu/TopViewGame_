using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickManager : MonoBehaviour
{
    public int goldCost = 50; // 레벨업에 필요한 골드 비용
    public void OpenLevelUpByGold()
    {

        if (GoldManager.Instance.currentGold >= goldCost)
        {
            GoldManager.Instance.AddGold(-goldCost);
            GameManager.instance.uiLevelUp.Show();
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
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