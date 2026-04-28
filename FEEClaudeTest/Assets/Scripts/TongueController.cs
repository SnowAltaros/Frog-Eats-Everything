using UnityEngine;

public class TongueController : MonoBehaviour
{
    private FrogController frog;
    private float speed;
    private float maxLength;

    private float currentLength = 0f;
    private bool retracting = false;
    private BugController caughtBug = null;

    private LineRenderer lineRenderer;
    private CircleCollider2D tipCollider;

    // Tip object (the actual hitbox that moves)
    private GameObject tip;

    public void Initialize(FrogController owner, float tongueSpeed, float tongueMaxLength)
    {
        frog = owner;
        speed = tongueSpeed;
        maxLength = tongueMaxLength;

        SetupTip();
        SetupLineRenderer();
    }

    void SetupTip()
    {
        tip = new GameObject("TongueTip");
        tip.transform.position = frog.transform.position;
        tip.layer = LayerMask.NameToLayer("Default");

        CircleCollider2D col = tip.AddComponent<CircleCollider2D>();
        col.radius = 0.2f;
        col.isTrigger = true;

        TongueTip tipScript = tip.AddComponent<TongueTip>();
        tipScript.owner = this;
    }

    void SetupLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.12f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = new Color(0.8f, 0.1f, 0.1f);
        lineRenderer.endColor = new Color(1f, 0.3f, 0.3f);
        lineRenderer.sortingOrder = 5;
    }

    void Update()
    {
        if (frog == null) { Destroy(gameObject); return; }

        Vector3 frogPos = frog.transform.position;
        Vector3 direction = frog.transform.up; // frog faces up by default

        if (!retracting)
        {
            // Extend tongue
            currentLength += speed * Time.deltaTime;

            if (currentLength >= maxLength)
            {
                currentLength = maxLength;
                retracting = true;
            }
        }
        else
        {
            // Retract tongue
            currentLength -= speed * 1.5f * Time.deltaTime;

            if (currentLength <= 0f)
            {
                currentLength = 0f;
                FinishTongue();
                return;
            }
        }

        // Move tip
        Vector3 tipPos = frogPos + direction * currentLength;
        tip.transform.position = tipPos;

        // If carrying a bug, move it with the tip
        if (caughtBug != null)
        {
            caughtBug.transform.position = tipPos;
        }

        // Draw line from frog to tip
        lineRenderer.SetPosition(0, frogPos);
        lineRenderer.SetPosition(1, tipPos);
    }

    public void HitBug(BugController bug)
    {
        if (retracting) return; // already retracting
        caughtBug = bug;
        bug.GetCaught();
        retracting = true;
    }

    void FinishTongue()
    {
        if (caughtBug != null)
        {
            // Eat the bug — give coins
            GameManager.Instance.AddCoins(caughtBug.coinValue);
            Destroy(caughtBug.gameObject);
            caughtBug = null;
        }

        if (tip != null) Destroy(tip);
        frog.OnTongueFinished();
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (tip != null) Destroy(tip);
    }
}
