using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Game Info")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public float exp;
    public int[] nextExp = { 3, 10, 20, 30, 40, 50, 70, 110, 150, 200, 250, 350, 500 };
    [Header("# GameObject")]
    public PoolManager pool;
    public Player_ player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;
    public GameObject StatusUI;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = maxHealth;

        Resume(); //���� ���� �� ���� ���� ����
        //�ӽ� ��ũ��Ʈ
        uiLevelUp.Select(0);
        AudioManager.Instance.PlayBgm(true); //������� ���
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine()); //���� ���� ��ƾ ����
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false; //���� ���� ���·� ����

        yield return new WaitForSeconds(1.5f);
        Stop(); //���� ����

        StatusUI.gameObject.SetActive(false); //���� UI ��Ȱ��ȭ
        uiResult.gameObject.SetActive(true); //���� ���� UI Ȱ��ȭ
        uiResult.Lose(); //���� ���� �޽��� ǥ��

        AudioManager.Instance.PlayBgm(false);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Lose); //���� ���� ���� ���

    }

    public void GameVictroy()
    {
        StartCoroutine(GameVictroyRoutine()); //���� ���� ��ƾ ����
    }

    IEnumerator GameVictroyRoutine()
    {
        isLive = false; //���� ���� ���·� ����
        enemyCleaner.SetActive(true); //�� ���ű� Ȱ��ȭ

        yield return new WaitForSeconds(0.4f);

        Stop(); //���� ����
        StatusUI.gameObject.SetActive(false); //���� UI ��Ȱ��ȭ
        uiResult.gameObject.SetActive(true); //���� ���� UI Ȱ��ȭ
        uiResult.Win();

        AudioManager.Instance.PlayBgm(false);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Win); //���� �¸� ���� ���

    }

    void Update()
    {
        if (!isLive) return; //������ ���� ���¸� �������� ����


        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictroy(); //���� �¸�
        }

    }

    public void GetExp(float score)
    {
        if (!isLive) return; //������ ���� ���¸� �������� ����

        exp += score;

        if(exp >= nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();

        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0f; //���� ����
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1f; //���� �簳
    }
}
