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
    public int exp;
    public int[] nextExp = { 3, 10, 20, 30, 40, 50, 70, 110, 150, 200, 250, 350, 500 };
    [Header("# GameObject")]
    public PoolManager pool;
    public Player_ player;
    public LevelUp uiLevelUp;
    public GameObject uiResult;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = maxHealth;

        //임시 스크립트
        uiLevelUp.Select(0);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine()); //게임 오버 루틴 시작
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false; //게임 정지 상태로 변경

        yield return new WaitForSeconds(1.5f);
        Stop(); //게임 정지
        uiResult.SetActive(true); //게임 오버 UI 활성화

    }

    void Update()
    {
        if (!isLive) return; //게임이 정지 상태면 실행하지 않음


        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }

    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();

        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0f; //게임 정지
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1f; //게임 재개
    }
}
