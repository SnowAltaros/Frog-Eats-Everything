using UnityEngine;

public class FrogController : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed = 120f; // degrees per second

    [Header("Tongue")]
    public float tongueSpeed = 8f;
    public float tongueMaxLength = 4f;
    public GameObject tonguePrefab;

    private TongueController activeTongue;
    private bool isShooting = false;

    void Update()
    {
        HandleRotation();
        HandleShoot();
    }

    void HandleRotation()
    {
        // Rotate frog to face mouse cursor
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float currentAngle = transform.eulerAngles.z;
        float targetAngle = angle;

        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }

    void HandleShoot()
    {
        if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            ShootTongue();
        }
    }

    void ShootTongue()
    {
        isShooting = true;

        // Spawn tongue at frog's position
        GameObject tongueObj = Instantiate(tonguePrefab, transform.position, transform.rotation);
        activeTongue = tongueObj.GetComponent<TongueController>();
        activeTongue.Initialize(this, tongueSpeed, tongueMaxLength);
    }

    public void OnTongueFinished()
    {
        isShooting = false;
        activeTongue = null;
    }

    // Called by UpgradeManager
    public void SetRotationSpeed(float speed) => rotationSpeed = speed;
    public void SetTongueSpeed(float speed) => tongueSpeed = speed;
    public void SetTongueLength(float length) => tongueMaxLength = length;
}
