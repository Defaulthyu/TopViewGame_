using UnityEngine;

public class MegaBullet : MonoBehaviour
{
    public int megaBulletPrefabId = 8; // PoolManager에서 MegaBullet의 인덱스
    public float megaBulletDamage = 100f;
    public int megaBulletPer = 10;
    public float megaBulletSpeed = 8f;
    public int cost = 200;

    Player_ player;

    void Start()
    {
        player = GameManager.instance.player;
    }

    // UI 버튼에 연결할 함수
    public void ShootMegaBullet()
    {
        if (GoldManager.Instance.currentGold < cost)
        {
            Debug.Log("골드 부족!");
            return;
        }

        if (!player.scanner.nearestTarget)
        {
            Debug.Log("타겟 없음!");
            return;
        }

        GoldManager.Instance.AddGold(-cost);

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = (targetPos - player.transform.position).normalized;

        Transform bullet = GameManager.instance.pool.Get(megaBulletPrefabId).transform;
        bullet.position = player.transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Init(megaBulletDamage, megaBulletPer, dir);  // 3개 인자 호출 유지

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = dir * megaBulletSpeed;  // 속도는 따로 여기서 직접 설정

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);
    }

}
