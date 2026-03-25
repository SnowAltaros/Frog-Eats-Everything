using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private float topPos = 6f;
    private float bottomPos = -6f;
    private float leftPos = -11f;
    private float rightPos = 11f;
    private bool isHitted;
    private Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPosition();
        direction = Random.insideUnitCircle.normalized;
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
        transform.rotation = Quaternion.Euler(0, 0 , 90);
    }

    private void RandomBottomSpawn()
    {
        float randomXPos = Random.Range(leftPos, rightPos);
        transform.position = new Vector2(randomXPos, bottomPos);
        transform.rotation =Quaternion.Euler(0, 0, -90);
    }

    private void RandomLeftSpawn()
    {
        float randomYPos = Random.Range(topPos, bottomPos);
        transform.position = new Vector2(leftPos, randomYPos);
        transform.rotation =Quaternion.Euler(0, 0, 180);
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
            //Destroy(gameObject);
            transform.rotation = Quaternion.Euler(0, 0, 180);
            direction = Random.insideUnitCircle.normalized;
        }
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
        }

        if (collision.gameObject.CompareTag("Boundary"))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            direction = Random.insideUnitCircle.normalized;
            Debug.Log("Collided with " + collision.gameObject.tag);
        }
    }
}
