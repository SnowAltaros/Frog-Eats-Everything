using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int health;
    [SerializeField] private Slider healthBar;
    [SerializeField] private string enemyType;
    [SerializeField] private int value;
    private SpawnManager spawnManager;
    private float topPos = 6f;
    private float bottomPos = -6f;
    private float leftPos = -11f;
    private float rightPos = 11f;
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
        if (transform.position.x < leftPos || transform.position.x > rightPos || transform.position.y < bottomPos || transform.position.y > topPos)
        {
            MoveToCenter();
        }
    }

    private void MoveToCenter()
    {
        Vector2 center = Vector2.zero;
        Vector2 randomDirection = Random.insideUnitCircle.normalized * 0.5f;
        direction = (center + randomDirection).normalized;
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
            health--;
            healthBar.value = health;

            if (health == 0)
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
            }
            else if (enemyType == "DragonFly")
            {
                spawnManager.maxDragonFly--;
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
