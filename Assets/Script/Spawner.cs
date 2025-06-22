using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public List<SpawnLevelData> levelSpawnDataList; // ���� �ν����Ϳ� ����!

    public float levelTime;
    int level;
    int prevLevel = -1; // ���� ������ �����ϴ� ����
    List<float> spawnTimers = new List<float>();



    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / levelSpawnDataList.Count;

        foreach (var data in levelSpawnDataList[0].spawnDatas)
            spawnTimers.Add(0f);
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), levelSpawnDataList.Count - 1);

        if (level != prevLevel)
        {
            Debug.Log($"[Spawner] �� ���� {level} ���� (���� �ð�: {GameManager.instance.gameTime:F1}s)");
            prevLevel = level;
        }

        SpawnData[] currentLevelData = levelSpawnDataList[level].spawnDatas;

        while (spawnTimers.Count < currentLevelData.Length)
            spawnTimers.Add(0f);

        for (int i = 0; i < currentLevelData.Length; i++)
        {
            spawnTimers[i] += Time.deltaTime;

            if (spawnTimers[i] > currentLevelData[i].spawnTime)
            {
                spawnTimers[i] = 0f;
                Spawn(currentLevelData[i]);
            }
        }
    }

    void Spawn(SpawnData data)
    {
        GameObject enemy = GameManager.instance.pool.Get(data.prefabId);
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
        enemy.GetComponent<Skull>().Init(data);
    }
}


[System.Serializable]
public class SpawnLevelData
{
    public SpawnData[] spawnDatas; // �̰� �ν����Ϳ� ������ �迭!
}



[System.Serializable]
public class SpawnData
{
    public int prefabId;
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;

    public float exp;
}
