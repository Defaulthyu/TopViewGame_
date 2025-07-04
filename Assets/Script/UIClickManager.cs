using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    Player_ player;
    public Button button; // UI 버튼
    public int cost = 50; //비활성화 할 코스트

    public AudioClip clickSound; //클릭 사운드
    private AudioSource audioSource; //오디오 소스

    void Start()
    {
        if(player != null)
            player = GameManager.instance.player;

        if (GoldManager .Instance != null)
            UpdateButton(GoldManager.Instance.currentGold);

        if (clickSound != null)
            audioSource = GetComponent<AudioSource>();
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

    public void OnClick()
    {
        audioSource.PlayOneShot(clickSound); // 클릭 사운드 재생
    }


}