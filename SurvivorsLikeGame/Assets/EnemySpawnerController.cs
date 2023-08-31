using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SpawnType
{
    Normal,
    Group,
}

[Serializable]
public class EnemySpawnData
{
    public string Title;

    public int ElapsedMinutes;
    public int ElapsedSeconds;

    public SpawnType SpawnType;

    public float SpawnDuration;

    public int SpawnCountMax;

    public List<int> EnemyIds;
}

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] List<EnemySpawnData> enemySpawnDatas;

    List<EnemyController> enemies;

    GameSceneDirector sceneDirector;

    Tilemap tilemapCollider;
    
    EnemySpawnData enemySpawnData;

    float oldSeconds;
    float spawnTimer;

    int spawnDataIndex;

    const float SpawnRadius = 13;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateEnemySpawnData();

        spawnEnemy();
    }

    public void Init(GameSceneDirector sceneDirector, Tilemap tilemapCollider)
    {
        this.sceneDirector = sceneDirector;
        this.tilemapCollider = tilemapCollider;

        Enemies = new List<EnemySpawnerController>();
        spawnDataIndex = -1;
    }

    void spawnEnemy()
    {
        if(null == enemySpawnData) return;

        spawnTimer -= Time.deltaTime;
        if (0 < spawnTimer) return;

        if (SpawnType.Group == enemySpawnData.SpawnType)
        {
            spawnGroup();
        }
        else if (SpawnType.Normal == enemySpawnData.SpawnType)
        {
            spawnNormal();
        }

        spawnTimer = enemySpawnData.SpawnDuration;
    }

    void spawnNormal()
    {
        Vector3 center = sceneDirector.Player.transform.position;

        for(int i=0 ; i<enemySpawnData.SpawnCountMax ; i++)
        {
            float angle = 360/enemySpawnData.SpawnCountMax * i;

            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * SpawnRadius;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * SpawnRadius;

            Vector2 pos = center + new Vector3(x,y,0);

            // TODO 当たり判定のあるタイル状なら生成しない
            if (Utils.IsCollisionTile(tilemapCollider, pos)) continue;

            createRandomEnemy(pos);
        }
    }

    void createRandomEnemy(Vector3 pos)
    {
        int rnd = Random.Range(0, enemySpawnData.EnemyIds.Count);
        int id = enemySpawnData.EnemyIds[rnd];

        EnemySpawnerController enemy = CharacterSettings.Instance.CreateEnemy(id, sceneDirector, pos);
        Enemies.Add(enemy);
    }

    void spawnGroup()
    {
        Vector3 center = sceneDirector.Player.transform.position;

        float angle = 360/enemySpawnData.SpawnCountMax * i;

        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * SpawnRadius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * SpawnRadius;

        center += new Vector3(x,y,0);
        float radius = 0.5f;

        for(int i=0 ; i<enemySpawnData.SpawnCountMax ; i++)
        {
            angle = Random.Range(0, 360);

            x = Mathf.Cos(angle * Mathf.Deg2Rad) * SpawnRadius;
            y = Mathf.Sin(angle * Mathf.Deg2Rad) * SpawnRadius;

            Vector2 pos = center + new Vector3(x,y,0);

            // TODO 当たり判定のあるタイル状なら生成しない
            if (Utils.IsCollisionTile(tilemapCollider, pos)) continue;

            createRandomEnemy(pos);
        }
    }

    void updateEnemySpawnData()
    {
        if (oldSeconds == sceneDirector.OldSeconds) return;

        int idx = spawnDataIndex + 1;

        if (enemySpawnDatas.Count - 1 < idx) return;

        EnemySpawnData data = enemySpawnDatas[idx];
        int elapsedSeconds = data.ElapsedMinutes * 60 + data.ElapsedSeconds;

        if (elapsedSeconds < sceneDirector.GameTimer)
        {
            enemySpawnData = enemySpawnDatas[idx];

            spawnDataIndex = idx;
            spawnTimer = 0;
            oldSeconds = sceneDirector.OldSeconds;
        }
    }

    public List<EnemySpawnerController> GetEnemies()
    {
        enemies.RemoveAll(item => !item);
        return enemies;
    }
}
