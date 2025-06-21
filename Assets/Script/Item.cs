using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
    }

    private void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);

        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;

            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
    }

    public void OnClicked()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    float deltaDamage = weapon.damage * data.damages[level]; // 누적 비율
                    int nextCount = data.counts[level];

                    weapon.LevelUp(deltaDamage, nextCount);
                }

                level++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if(level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                    level++;
                    gear.LevelUp(gear.rate * (1 + data.damages[0])); // 초기 레벨업
                }
                else
                {
                    //float deltaRate = gear.rate * data.damages[level];
                    //gear.LevelUp(gear.rate * (1 + data.damages[level]));
                    gear.rate *= (1 + data.damages[level]);
                    gear.LevelUp(gear.rate);
                    level++;
                }

                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health = Mathf.Min(GameManager.instance.health + 30, GameManager.instance.maxHealth);
                break;
        }


        if(level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
    
}
