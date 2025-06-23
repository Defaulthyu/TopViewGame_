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

        //임시 스크립트
        uiLevelUp.Select(0);
        AudioManager.Instance.PlayBgm(true); //배경음악 재생
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

        StatusUI.gameObject.SetActive(false); //상태 UI 비활성화
        uiResult.gameObject.SetActive(true); //게임 오버 UI 활성화
        uiResult.Lose(); //게임 오버 메시지 표시

        AudioManager.Instance.PlayBgm(false);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Lose); //게임 오버 사운드 재생

    }

    public void GameVictroy()
    {
        StartCoroutine(GameVictroyRoutine()); //게임 오버 루틴 시작
    }

    IEnumerator GameVictroyRoutine()
    {
        isLive = false; //게임 정지 상태로 변경
        enemyCleaner.SetActive(true); //적 제거기 활성화

        yield return new WaitForSeconds(0.1f);

        Stop(); //게임 정지
        StatusUI.gameObject.SetActive(false); //상태 UI 비활성화
        uiResult.gameObject.SetActive(true); //게임 오버 UI 활성화
        uiResult.Win();

        AudioManager.Instance.PlayBgm(false);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Win); //게임 승리 사운드 재생

    }

    void Update()
    {
        if (!isLive) return; //게임이 정지 상태면 실행하지 않음


        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictroy(); //게임 승리
        }

    }

    public void GetExp(float score)
    {
        if (!isLive) return; //게임이 정지 상태면 실행하지 않음

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
        Time.timeScale = 0f; //게임 정지
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1f; //게임 재개
    }
}
