using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float spawnRate = 1.5f;
    public int maxFly;
    public int maxDragonFly;
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
                if (maxFly < 5)
                {
                    Instantiate(enemies[0]);
                    maxFly++;
                    totalSpawned++;
                }
                else if (maxDragonFly < 5)
                {
                    Instantiate(enemies[1]);
                    maxDragonFly++;
                    totalSpawned++;
                }
            }   
        }
    }
}
