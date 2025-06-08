using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //프리펩들을 보관할 변수
    public GameObject[] prefabs;

    //풀 담당을 하는 리스트들
    List<GameObject>[] pools;

    void Awake()
    {
        //풀의 개수는 프리펩의 개수와 동일
        pools = new List<GameObject>[prefabs.Length];
        
        for(int index = 0; index < pools.Length; index++)
        {
            //각 풀을 초기화
            pools[index] = new List<GameObject>();
        }

    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        //선택한 풀의 놀고 (비활성화 된) 있는 게임 오브젝트 접근


        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {        
                //발견하면 select 변수에 저장
                select = item;
                select.SetActive(true); //활성화
                break;
            }
        }

        //못찾으면

        if (!select)
        {
            //새롭게 생성
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select); //풀에 추가
        }

        return select; //활성화 된 게임 오브젝트 반환

    }


}
