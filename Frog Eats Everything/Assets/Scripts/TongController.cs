using UnityEngine;
using UnityEngine.UIElements;

public class TongController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private float tongueLength = 5f;
    private Vector3 startTongueLength;
    private float timer = 0.8f;
    private bool isTongueOut;

    void Start()
    {
        startTongueLength = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        ShootTongue();   
    }

    private void ShootTongue()
    {
        bool mouseClicked = inputHandler.ClickTriggered;

        if (mouseClicked && !isTongueOut)
        {
            Vector3 scale = transform.localScale;
            scale.y = tongueLength;
            transform.localScale = scale;
            isTongueOut = true;
        }

        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            transform.localScale = startTongueLength;
            timer = 0.8f;
            isTongueOut = false;
        }
    }
}
