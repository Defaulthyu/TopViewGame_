using UnityEngine;

public class HelpPopupManager : MonoBehaviour
{
    public GameObject helpPopupUI;  // Inspector에서 연결할 팝업 UI 오브젝트

    const string FirstPopUp = "FirstPopUp";

    void Start()
    {
        // PlayerPrefs에 저장된 값이 없으면, 처음 접속한 것임
        if (!PlayerPrefs.HasKey(FirstPopUp))
        {
            helpPopupUI.SetActive(true);
        }
        else
        {
            helpPopupUI.SetActive(false);
        }
    }

    public void OnClick_DoNotShowAgain()
    {
        PlayerPrefs.SetInt(FirstPopUp, 1);  // 다시 보지 않음 설정
        PlayerPrefs.Save();
        helpPopupUI.SetActive(false);
    }
}