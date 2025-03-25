using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnRadius = 10f;
    public float spawnRate = 3f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, spawnRate);
    }

    void SpawnEnemy()
    {
        if (player == null || enemyPrefab == null) return;

        Vector3 spawnPosition = player.position + GetRandomPosition();
        spawnPosition.y = player.position.y;

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Gán Player vào EnemyAI script của quái
        EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.player = player;
        }
    }

    Vector3 GetRandomPosition()
    {
        float randomAngle = Random.Range(0f, 360f);
        float randomDistance = Random.Range(3f, spawnRadius);

        float spawnX = Mathf.Cos(randomAngle) * randomDistance;
        float spawnZ = Mathf.Sin(randomAngle) * randomDistance;

        return new Vector3(spawnX, 0, spawnZ);
    }
}
