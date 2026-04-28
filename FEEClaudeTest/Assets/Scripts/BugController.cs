using UnityEngine;

public class BugController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public float directionChangeInterval = 1.5f;
    public float boundaryRadius = 7f; // stay within this radius from center

    [Header("Reward")]
    public int coinValue = 10;

    private Vector2 moveDirection;
    private float directionTimer;
    private bool isCaught = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f;
        }
        PickNewDirection();
        // Randomize starting timer so not all bugs change direction at once
        directionTimer = Random.Range(0f, directionChangeInterval);
    }

    void Update()
    {
        if (isCaught) return;

        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0f)
        {
            PickNewDirection();
        }

        // Push back toward center if too far
        if (transform.position.magnitude > boundaryRadius)
        {
            Vector2 toCenter = -((Vector2)transform.position).normalized;
            moveDirection = Vector2.Lerp(moveDirection, toCenter, 0.3f).normalized;
        }

        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        // Rotate the bug to face movement direction (visual flair)
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void PickNewDirection()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        directionTimer = Random.Range(directionChangeInterval * 0.5f, directionChangeInterval * 1.5f);
    }

    public void GetCaught()
    {
        isCaught = true;
        // Disable movement, let tongue drag it
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }
}
