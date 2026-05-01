using UnityEngine;

public class TongController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] public float tongueLength;
    [SerializeField] public float speed;
    [SerializeField] public int strength;
    
    private Vector3 startTongueLength;
    private bool isTongueOut;
    public bool isShooted;
    private bool isCollided;

    void Start()
    {
        StartingParameters();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.mouseWasClicked)
        {
            ShootTongue();
        }

        RollBackTongue();
    }

    private void StartingParameters()
    {
        startTongueLength = transform.localScale;
        tongueLength = PlayerStats.tongueLength;
        speed = PlayerStats.tongueSpeed;
        strength = PlayerStats.tongueStength;
    }

    private void ShootTongue()
    {
        if (!isTongueOut)
        {
            isShooted = true;
            Vector3 scale = transform.localScale;
            scale.y = tongueLength;
            Vector3 targetScale = Vector3.MoveTowards(transform.localScale, scale, speed * Time.deltaTime);
            transform.localScale = targetScale;

            if (transform.localScale.y == scale.y || isCollided)
            {
                isTongueOut = true;
                inputHandler.mouseWasClicked = false;
            }
        }
    }

    private void RollBackTongue()
    {
        if (isTongueOut)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, startTongueLength, speed * Time.deltaTime);

            if (transform.localScale.y == startTongueLength.y)
            {
                isTongueOut = false;
                isShooted = false;
                isCollided = false;
            }
        }
    }

    public void OnChildTrigger(Collider2D collision)
    {
        isCollided = true;
    }
}
