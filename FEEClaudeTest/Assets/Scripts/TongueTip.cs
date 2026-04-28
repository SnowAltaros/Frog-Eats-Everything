using UnityEngine;

// Attached to the moving tip of the tongue to detect bug collisions
public class TongueTip : MonoBehaviour
{
    public TongueController owner;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (owner == null) return;

        BugController bug = other.GetComponent<BugController>();
        if (bug != null)
        {
            owner.HitBug(bug);
        }
    }
}
