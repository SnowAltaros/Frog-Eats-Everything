using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int health;
    [SerializeField] private Slider healthBar;
    [SerializeField] private string enemyType;
    [SerializeField] private int value;
    private SpawnManager spawnManager;
    private float topPos = 9f;
    private float bottomPos = -9f;
    private float leftPos = -15f;
    private float rightPos = 15f;
    private bool isHitted;
    private Transform tongueEnd;
    private Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPosition();
        GetMovingDirection();
        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>().GetComponent<SpawnManager>();
        healthBar.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHitted)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }

        if(isHitted && tongueEnd != null)
        {
            transform.position = tongueEnd.position;
        }
        ChangeDirectionOutsideScreen();
    }

    private void SpawnPosition()
    {
        float[] positions = {topPos, bottomPos, leftPos, rightPos};
        int index = Random.Range(0, positions.Length);

        switch (index)
        {
            case 0:
                RandomTopSpawn();
                break;
            case 1:
                RandomBottomSpawn();
                break;
            case 2:
                RandomLeftSpawn();
                break;
            case 3:
                RandomRightSpawn();
                break;
        }
    }

    private void RandomTopSpawn()
    {
        float randomXPos = Random.Range(leftPos, rightPos);
        transform.position = new Vector2(randomXPos, topPos);
    }

    private void RandomBottomSpawn()
    {
        float randomXPos = Random.Range(leftPos, rightPos);
        transform.position = new Vector2(randomXPos, bottomPos);
    }

    private void RandomLeftSpawn()
    {
        float randomYPos = Random.Range(topPos, bottomPos);
        transform.position = new Vector2(leftPos, randomYPos);
    }

    private void RandomRightSpawn()
    {
        float randomYPos = Random.Range(topPos, bottomPos);
        transform.position = new Vector2(rightPos, randomYPos);
    }

    private void ChangeDirectionOutsideScreen()
    {
        if (transform.position.x > rightPos && direction.x > 0)
            MoveToCenter();
        else if (transform.position.x < leftPos && direction.x < 0)
            MoveToCenter();
        else if (transform.position.y > topPos && direction.y > 0)
            MoveToCenter();
        else if (transform.position.y < bottomPos && direction.y < 0)
            MoveToCenter();
    }

    private void MoveToCenter()
    {
        Vector2 center = Vector2.zero;
        Vector2 directionToCenter = ((Vector2)transform.position - center).normalized * -1f;
        Vector2 randomOffset = Random.insideUnitCircle.normalized;

        // Adjust the blend: higher centerBias = moves to center more frequently
        float centerBias = 0.7f; // 0 = fully random, 1 = always toward center
        direction = (directionToCenter * centerBias + randomOffset * (1f - centerBias)).normalized;
        transform.up = direction;
    }

    private void GetMovingDirection()
    {
        direction = Random.insideUnitCircle.normalized;
        transform.up = direction;
    }
    private void ReverseDirection()
    {
        direction = -direction;
        transform.up = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tongue"))
        {
            TongController tongue = collision.gameObject.GetComponentInParent<TongController>();

            if (tongue.strength > 0)
            {
                health -= tongue.strength;
                health = Mathf.Max(health, 0);
                healthBar.value = health;
            }

            if (health <= 0)
            {
                isHitted = true;
                tongueEnd = collision.gameObject.transform;
                transform.position = tongueEnd.position;
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            spawnManager.totalSpawned --;

            if (enemyType == "Fly")
            {
                spawnManager.maxFly--;
                PlayerStats.wings += value;
            }
            else if (enemyType == "DragonFly")
            {
                spawnManager.maxDragonFly--;
                PlayerStats.wings += value;
            }
        }

        if (collision.gameObject.CompareTag("Boundary"))
        {
            if (!isHitted)
            {
                ReverseDirection();
            }
        }
    }
}
