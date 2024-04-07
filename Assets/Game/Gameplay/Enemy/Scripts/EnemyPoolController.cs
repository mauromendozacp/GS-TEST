using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;

public class EnemyPoolController : MonoBehaviour
{
    [SerializeField] private EnemyController enemyPrefab = null;
    [SerializeField] private Transform holder = null;
    [SerializeField] private float spawnTimer = 0f;
    [SerializeField] private float leftLimitX = 0f;
    [SerializeField] private float rightLimitX = 0f;

    private ObjectPool<EnemyController> enemyPool = null;
    private List<EnemyController> enemiesSpawned = null;
    private bool stopSpawn = false;

    private void Start()
    {
        enemyPool = new ObjectPool<EnemyController>(CreateEnemy, GetEnemy, ReleaseEnemy, DestroyEnemy, defaultCapacity: 5, maxSize: 5);
        enemiesSpawned = new List<EnemyController>();

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (!stopSpawn)
        {
            EnemyController enemy = enemyPool.Get();
            enemy.transform.position = new Vector2(Random.Range(leftLimitX, rightLimitX), enemy.transform.position.y);

            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private EnemyController CreateEnemy()
    {
        EnemyController enemy = Instantiate(enemyPrefab, holder);
        enemy.Init(ReleaseEnemy);

        return enemy;
    }

    private void GetEnemy(EnemyController enemy)
    {
        enemy.Get();
        enemiesSpawned.Add(enemy);
    }

    private void ReleaseEnemy(EnemyController enemy)
    {
        enemy.Release();
        enemiesSpawned.Remove(enemy);
    }

    private void DestroyEnemy(EnemyController enemy)
    {
        Destroy(enemy.gameObject);
    }
}
