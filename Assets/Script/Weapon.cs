using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        //�ʱ�ȭ
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0: //��
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            default:

                break;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //������
            LevelUp(10, 3);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage += damage;
        this.count += count;
        //�������� �ʱ�ȭ
        if(id == 0)
            Batch();
    }

    public void Init()
    {
        switch (id)
        {
            case 0: //��
                speed = 150;
                Batch();
                break;

            default:

                break;
        }
    }

    void Batch()
    {
        for (int index = 0; index < count; index++)
        {

            Transform bullet;
            if(index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.parent = transform;

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 0.3f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1);     //���� ����
            
        }
    }
}
