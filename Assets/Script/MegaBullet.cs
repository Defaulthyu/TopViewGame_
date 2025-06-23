using UnityEngine;
using UnityEngine.UI;

public class MegaBullet : MonoBehaviour
{
    public int megaBulletPrefabId = 8; // PoolManager���� MegaBullet�� �ε���
    public float megaBulletDamage = 100f;
    public int megaBulletPer = 10;
    public float megaBulletSpeed = 8f;
    public int cost = 200;

    public Button megaShotButton; // UI ��ư

    Player_ player;

    void Start()
    {
        player = GameManager.instance.player;
        UpdateButton(GoldManager.Instance.currentGold);
    }

    private void OnEnable()
    {
        GoldManager.OnGoldChanged += UpdateButton;
    }

    private void OnDisable()
    {
        GoldManager.OnGoldChanged -= UpdateButton;
    }

    void UpdateButton(int gold)
    {
        if(megaShotButton != null)
            megaShotButton.interactable = gold >= cost;
    }

    // UI ��ư�� ������ �Լ�
    public void ShootMegaBullet()
    {
        if (GoldManager.Instance.currentGold < cost)
        {
            Debug.Log("��� ����!");
            return;
        }

        if (!player.scanner.nearestTarget)
        {
            Debug.Log("Ÿ�� ����!");
            return;
        }

        GoldManager.Instance.AddGold(-cost);

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = (targetPos - player.transform.position).normalized;

        Transform bullet = GameManager.instance.pool.Get(megaBulletPrefabId).transform;
        bullet.position = player.transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Init(megaBulletDamage, megaBulletPer, dir);  // 3�� ���� ȣ�� ����

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = dir * megaBulletSpeed;  // �ӵ��� ���� ���⼭ ���� ����

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);
    }

}
