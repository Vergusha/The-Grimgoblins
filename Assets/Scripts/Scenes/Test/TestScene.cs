using UnityEngine;
using TMPro;

public class TestScene : MonoBehaviour
{
	public GameTimer gameTimer;
	public TMP_Text timerText;
	public GameObject enemyPrefab;
	// spawnPoints больше не нужен
	public float spawnInterval = 5f;
	public Camera mainCamera;

	private float lastSpawnTime = 0f;
	private bool spawningActive = true;

	private void Update()
	{
		if (gameTimer == null) return;
		if (!spawningActive) return;

		float elapsed = gameTimer.GetElapsedTime();
		UpdateTimerText(elapsed);
		if (elapsed >= 25 * 60f)
		{
			spawningActive = false;
			EndPanelController panel = FindFirstObjectByType<EndPanelController>();
			if (panel != null)
				panel.ShowPanel();
			return;
		}

		if (Time.time - lastSpawnTime >= spawnInterval)
		{
			lastSpawnTime = Time.time;
			SpawnEnemyAtScreenEdge();
		}
	}

	private void UpdateTimerText(float elapsed)
	{
		if (timerText != null)
		{
			int minutes = Mathf.FloorToInt(elapsed / 60f);
			int seconds = Mathf.FloorToInt(elapsed % 60f);
			timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
		}
	}

	private void SpawnEnemyAtScreenEdge()
	{
		if (enemyPrefab == null || mainCamera == null) return;

		// Получаем границы экрана в мировых координатах
		float camZ = mainCamera.transform.position.z;
		Vector3 camPos = mainCamera.transform.position;
		float distance = Mathf.Abs(camZ);
		Vector3 left = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, distance));
		Vector3 right = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, distance));
		Vector3 top = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1, distance));
		Vector3 bottom = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, distance));

		// Выбираем случайную сторону
		int side = Random.Range(0, 4); // 0=left, 1=right, 2=top, 3=bottom
		Vector3 spawnPos = Vector3.zero;
		float offset = 2f; // Насколько дальше от края спавнить
		switch (side)
		{
			case 0: // left
				spawnPos = new Vector3(left.x - offset, Random.Range(bottom.y, top.y), 0);
				break;
			case 1: // right
				spawnPos = new Vector3(right.x + offset, Random.Range(bottom.y, top.y), 0);
				break;
			case 2: // top
				spawnPos = new Vector3(Random.Range(left.x, right.x), top.y + offset, 0);
				break;
			case 3: // bottom
				spawnPos = new Vector3(Random.Range(left.x, right.x), bottom.y - offset, 0);
				break;
		}

		Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
	}
}
