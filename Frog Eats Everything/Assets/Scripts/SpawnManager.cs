using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float spawnRate = 1.5f;
    private int maxSpawn = 10;
    public int totalSpawned = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            if (totalSpawned <= maxSpawn)
            {
                Instantiate(enemy);
                totalSpawned++;
            }   
        }
    }
}
