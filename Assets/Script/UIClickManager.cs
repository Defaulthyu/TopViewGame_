using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


}