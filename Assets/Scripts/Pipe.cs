using UnityEngine;

public class Pipe : Obstacle
{
    public Vector2 direction = Vector2.left;

    public Transform topPipe;
    public Transform bottomPipe;
    public BoxCollider2D triggerReward;
    [SerializeField] private float despawnX;
    

    [Header("Giới hạn vị trí trung tâm khoảng hở")]
    public float minCenterY = 0f;
    public float maxCenterY = 4f;

    public float minGap = 10f;
    protected override void Awake()
    {
        base.Awake();
        topPipe = transform.Find("topPipe");
        bottomPipe = transform.Find("bottomPipe");
        triggerReward = transform.Find("triggerReward").GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        RandomizeGapPosition();
    }
    private void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.fixedDeltaTime);

        if (transform.position.x < despawnX)
        {
            ReturnToPool();
        }
    }

    public void RandomizeGapPosition()
    {
        float bottomRandCenterY = Random.Range(minCenterY, maxCenterY);

        bottomPipe.localPosition = new Vector2(bottomPipe.localPosition.x, bottomRandCenterY);

        float topRandCenterY = Random.Range(minGap + bottomRandCenterY, minGap + maxCenterY);
        topPipe.localPosition = new Vector2(topPipe.localPosition.x, topRandCenterY);

        float triggerCenterY = bottomRandCenterY + (topRandCenterY - bottomRandCenterY) / 2;
        triggerReward.transform.localPosition = new Vector2(triggerReward.transform.localPosition.x, triggerCenterY);

        triggerReward.size = new Vector2(0.5f, triggerCenterY*2);
    }
}
