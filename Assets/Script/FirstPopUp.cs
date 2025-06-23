using UnityEngine;

public class HelpPopupManager : MonoBehaviour
{
    public GameObject helpPopupUI;  // Inspector���� ������ �˾� UI ������Ʈ

    const string FirstPopUp = "FirstPopUp";

    void Start()
    {
        // PlayerPrefs�� ����� ���� ������, ó�� ������ ����
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
        PlayerPrefs.SetInt(FirstPopUp, 1);  // �ٽ� ���� ���� ����
        PlayerPrefs.Save();
        helpPopupUI.SetActive(false);
    }
}