using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                //myText.text = GameManager.instance.player.exp.ToString();
                break;
            case InfoType.Level:
                //myText.text = GameManager.instance.player.level.ToString();
                break;
            case InfoType.Kill:
                //myText.text = GameManager.instance.player.killCount.ToString();
                break;
            case InfoType.Time:
                //myText.text = GameManager.instance.playTime.ToString("F2");
                break;
            case InfoType.Health:
                //mySlider.value = GameManager.instance.player.health / GameManager.instance.player.maxHealth;
                break;
        }
    }
}
