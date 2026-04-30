using UnityEngine;

public class FrogRendered : MonoBehaviour
{
    [SerializeField] private Sprite closedMouth;
    [SerializeField] private Sprite openMouth;
    
    [SerializeField] private SpriteRenderer frog;

    [SerializeField] private TongController tongueController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frog.sprite = closedMouth;
    }

    // Update is called once per frame
    void Update()
    {
        if (tongueController.isShooted)
        {
            frog.sprite = openMouth;
        }
        else
        {
            frog.sprite = closedMouth;
        }
    }
}
