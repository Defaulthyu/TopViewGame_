using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //��������� ������ ����
    public GameObject[] prefabs;

    //Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] pools;

    void Awake()
    {
        //Ǯ�� ������ �������� ������ ����
        pools = new List<GameObject>[prefabs.Length];
        
        for(int index = 0; index < pools.Length; index++)
        {
            //�� Ǯ�� �ʱ�ȭ
            pools[index] = new List<GameObject>();
        }

    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        //������ Ǯ�� ��� (��Ȱ��ȭ ��) �ִ� ���� ������Ʈ ����


        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {        
                //�߰��ϸ� select ������ ����
                select = item;
                select.SetActive(true); //Ȱ��ȭ
                break;
            }
        }

        //��ã����

        if (!select)
        {
            //���Ӱ� ����
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select); //Ǯ�� �߰�
        }

        return select; //Ȱ��ȭ �� ���� ������Ʈ ��ȯ

    }


}
