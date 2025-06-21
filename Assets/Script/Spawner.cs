using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public SpawnData[] spawnData;
    public float levelTime;


    int level;

    float timer;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;
    }
    void Update()
    {
        if(!GameManager.instance.isLive)
        {
            return;
        }
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length - 1);

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }

    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(spawnData[level].prefabId);
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
        enemy.GetComponent<Skull>().Init(spawnData[level]);
        Debug.Log("SpawnData prefabId: " + spawnData[level].prefabId);

    }
}

[System.Serializable]
public class SpawnData
{
    public int prefabId;
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}
