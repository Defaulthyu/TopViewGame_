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

    float timer;
    Player_ player;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        switch (id)
        {
            case 0: //삽
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            default:
                timer += Time.deltaTime;

                if(timer > speed)
                {
                    timer = 0;
                    Fire();
                }
                break;
        }
        //test code
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //레벨업
            LevelUp(10, 3);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage += damage;
        this.count += count;
        //레벨업시 초기화
        if(id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); //무기 장착시 플레이어에게 알림
    }

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for(int index = 0; index < GameManager.instance.pool. prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }
        switch (id)
        {
            case 0: //삽
                speed = 150;
                Batch();
                break;

            case 1: //총
                speed = 2f; //총알 발사 속도
                break;

            default:
                speed = 0.3f;
                break;
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); //무기 장착시 플레이어에게 알림
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
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);     //무한 관통
            
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
