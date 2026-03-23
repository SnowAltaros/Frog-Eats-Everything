using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    // private Vector3 movingDirection;
    private float topPos = 6f;
    private float bottomPos = -6f;
    private float leftPos = -11f;
    private float rightPos = 11f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        Destroy();
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

    private void Destroy()
    {
        if (transform.position.x < leftPos || transform.position.x > rightPos || transform.position.y < bottomPos || transform.position.y > topPos)
        {
            Destroy(gameObject);
        }
    }
}
