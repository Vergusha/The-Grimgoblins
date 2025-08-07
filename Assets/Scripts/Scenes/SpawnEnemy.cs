using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject manekenPrefab;
    public int spawnCount = 5;
    public float spawnDistance = 20f; // Дистанция от камеры для спавна

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        var spawnPositions = new System.Collections.Generic.List<Vector3>();
        int maxAttempts = 100;
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPos = Vector3.zero;
            bool validPos = false;
            int attempts = 0;
            while (!validPos && attempts < maxAttempts)
            {
            int side = Random.Range(0, 4); // 0=left, 1=right, 2=top, 3=bottom
            float x, y;
            float offset = 2.0f; // увеличенный отступ для спавна вне зоны видимости
            switch (side)
            {
                case 0: // left
                    x = -offset;
                    y = Random.Range(0.0f, 1.0f);
                    break;
                case 1: // right
                    x = 1.0f + offset;
                    y = Random.Range(0.0f, 1.0f);
                    break;
                case 2: // top
                    x = Random.Range(0.0f, 1.0f);
                    y = 1.0f + offset;
                    break;
                default: // bottom
                    x = Random.Range(0.0f, 1.0f);
                    y = -offset;
                    break;
            }
            Vector3 spawnViewport = new Vector3(x, y, Camera.main.nearClipPlane + spawnDistance);
                spawnPos = Camera.main.ViewportToWorldPoint(spawnViewport);
                spawnPos.y = transform.position.y;
                validPos = true;
                foreach (var pos in spawnPositions)
                {
                    if (Vector3.Distance(pos, spawnPos) < 1.5f)
                    {
                        validPos = false;
                        break;
                    }
                }
                attempts++;
            }
            spawnPositions.Add(spawnPos);
            GameObject enemy = Instantiate(manekenPrefab, spawnPos, Quaternion.identity);
        }
    }
}