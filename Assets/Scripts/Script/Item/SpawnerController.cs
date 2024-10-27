using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private bool canSpawn = true;
    [SerializeField] private int maxEnemies = 3;
    [SerializeField] private float spawnRadius = 5f;
    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;

            if (enemies.Count < maxEnemies)
            {

                Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                enemies.Add(newEnemy);

                newEnemy.GetComponent<EnemyController>().OnEnemyDestroyed += RemoveEnemyFromList;
            }
        }
    }

    private void RemoveEnemyFromList(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }
}
