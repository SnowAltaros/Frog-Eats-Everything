using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private SpawnManager spawnManager;
    private float topPos = 6f;
    private float bottomPos = -6f;
    private float leftPos = -11f;
    private float rightPos = 11f;
    private bool isHitted;
    private Vector2 direction;
    private float angle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPosition();
        GetMovingDirection();
        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>().GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHitted)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        DestroyOutsideScreen();
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

    private void DestroyOutsideScreen()
    {
        if (transform.position.x < leftPos || transform.position.x > rightPos || transform.position.y < bottomPos || transform.position.y > topPos)
        {
            direction = Random.insideUnitCircle.normalized;
        }
    }

    private void GetMovingDirection()
    {
        direction = Random.insideUnitCircle.normalized;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tongue"))
        {
            isHitted = true;
            transform.SetParent(collision.transform);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            spawnManager.totalSpawned --;
        }

        if (collision.gameObject.CompareTag("Boundary"))
        {
            direction = Random.insideUnitCircle.normalized;
        }
    }
}
