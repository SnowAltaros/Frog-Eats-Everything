using UnityEngine;

public class TongController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private float tongueLength = 5f;
    [SerializeField] private float speed = 30;
    private Vector3 startTongueLength;
    private bool isTongueOut;
    public bool isShooted;

    void Start()
    {
        startTongueLength = transform.localScale;
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

    private void ShootTongue()
    {
        if (!isTongueOut)
        {
            isShooted = true;
            Vector3 scale = transform.localScale;
            scale.y = tongueLength;
            Vector3 targetScale = Vector3.MoveTowards(transform.localScale, scale, speed * Time.deltaTime);
            transform.localScale = targetScale;

            if (transform.localScale.y == scale.y)
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
            }
        }
    }
}
