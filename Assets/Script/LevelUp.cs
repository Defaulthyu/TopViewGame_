using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp); //레벨업 사운드 재생
        AudioManager.Instance.EffectBgm(true); //배경음 효과 활성화
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select); //레벨업 사운드 재생
        AudioManager.Instance.EffectBgm(false); //배경음 효과 비활성화
    }

    public void Select(int index)
    {
        items[index].OnClicked();
    }

    void Next()
    {
        //모든 아이템 비활성화
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false);
        }
        //그 중에서 랜덤 3개 아이템 활성화
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int index = 0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];
            //만렙 아이템의 경우는 소비 아이템으로 대체
            if(ranItem.level == ranItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true); //소비 아이템 활성화
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }

        }
    }
}
