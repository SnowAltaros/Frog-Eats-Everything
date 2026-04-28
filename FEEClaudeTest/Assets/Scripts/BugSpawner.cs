using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public GameObject bugPrefab;
    public int maxBugs = 10;
    public float spawnInterval = 2f;
    public float spawnRadius = 6f;

    private float spawnTimer;
    private int currentBugCount = 0;

    void Update()
    {
        // Count living bugs
        currentBugCount = FindObjectsByType<BugController>(FindObjectsSortMode.None).Length;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f && currentBugCount < maxBugs)
        {
            SpawnBug();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnBug()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 spawnPos = new Vector3(
            Mathf.Cos(angle) * spawnRadius,
            Mathf.Sin(angle) * spawnRadius,
            0f
        );

        Instantiate(bugPrefab, spawnPos, Quaternion.identity);
    }
}
