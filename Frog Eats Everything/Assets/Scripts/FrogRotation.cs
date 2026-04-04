using UnityEngine;

public class FrogRotation : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private TongController tongueController;
    [SerializeField] public float speed;
    private float lastSpeed;

    private void Start()
    {
        speed = PlayerStats.frogRotationSpeed;
        lastSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpeedChange();

        if (!tongueController.isShooted)
        {
            RotateToMousePos();
        }
    }

    private void RotateToMousePos()
    {
        // mousePos stores the mouse world position in PlayerInputHandler
        Vector2 mousePos = inputHandler.MousePosition;

        // Mathematic function to find the angle of rotation, 
        // Mathf.Atan2 return the angles in radians
        // Mathf.Rad2Deg transforms radian to degrees
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;

        // Setting the target rotation
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        // Adding speed and rotate to target position from initial
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    private void CheckSpeedChange()
    {
        if (PlayerStats.frogRotationSpeed != lastSpeed)
        {
            speed = PlayerStats.frogRotationSpeed;
            lastSpeed = speed;
        }
    }
}
