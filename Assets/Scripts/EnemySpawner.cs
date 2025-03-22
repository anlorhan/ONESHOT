using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform worldCenter;
    public float spawnRate = 2f;
    public float difficultyIncreaseRate = 30f;
    private float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        // Zamanla düşman üretim süresini azalt ve zorluk artır
        if (spawnTimer >= spawnRate)
        {
            SpawnEnemy();
            spawnTimer = 0f;

            // Zorluk artışı
            spawnRate = Mathf.Max(0.5f, spawnRate - 0.05f);  // En az 0.5 saniye olmasını sağlar
        }
    }

    void SpawnEnemy()
    {
        // Rasgele bir pozisyonda düşman üret
        Vector2 spawnPosition = (Vector2)worldCenter.position + Random.insideUnitCircle.normalized * 10f;
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<Enemy>().worldCenter = worldCenter;
    }
}

