using UnityEngine;

public class TongueEndCollision : MonoBehaviour
{
    private TongController tongue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tongue = GetComponentInParent<TongController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tongue.OnChildTrigger(collision);
    }
}
