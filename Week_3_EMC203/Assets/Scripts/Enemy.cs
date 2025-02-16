using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2f;
    public bool useBezierCurve = true; // Use Bezier curve movement

    public GameObject goldCoinPrefab; // Assign in Inspector
    public Transform goldTarget;      // Assign in Inspector
    public float goldCoinSpeed = 7f;  // Speed for coin movement

    private float t = 0f;
    private Vector3 startPos;
    private Vector3 controlPoint; // Used for Bezier curve

    // Wave and spawning variables
    public static int currentWave = 1;
    public static int enemiesLeft = 0;
    public static int enemiesToSpawn = 10;

    private static Spawner spawner; // Reference to the spawner
    private static TextMeshProUGUI enemiesLeftText; // UI Text for enemies left

    void Start()
    {
        target = GameObject.Find("TargetPoint")?.transform;
        goldTarget = GameObject.Find("GoldTarget")?.transform;
        spawner = GameObject.FindFirstObjectByType<Spawner>();
        enemiesLeftText = GameObject.FindFirstObjectByType<TextMeshProUGUI>();

        if (target == null)
            Debug.LogError("TargetPoint GameObject not found!");
        if (goldTarget == null)
            Debug.LogError("GoldTarget GameObject not found!");
        if (spawner == null)
            Debug.LogError("Spawner GameObject not found!");

        startPos = transform.position;
        controlPoint = (startPos + target.position) / 2 + new Vector3(2, 2, 0); // Adjusted curve height
        enemiesLeft++;

        UpdateEnemiesLeftUI();
    }

    void Update()
    {
        if (target == null) return;

        t += Time.deltaTime * moveSpeed;
        transform.position = useBezierCurve ? BezierCurve(startPos, controlPoint, target.position, t) : QuadraticLerp(startPos, target.position, t);

        if (t >= 1f)
        {
            GameManager.Instance.DecreaseHP();
            Destroy(gameObject);
        }
    }

    Vector3 QuadraticLerp(Vector3 start, Vector3 end, float t)
    {
        return Vector3.Lerp(start, end, t * t);
    }

    Vector3 BezierCurve(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        Vector3 p0 = Vector3.Lerp(start, control, t);
        Vector3 p1 = Vector3.Lerp(control, end, t);
        return Vector3.Lerp(p0, p1, t);
    }

    public void SpawnGoldCoin()
    {
        if (goldCoinPrefab == null || goldTarget == null)
        {
            Debug.LogError("GoldCoin Prefab or Target is NULL!");
            return;
        }

        GameObject coin = Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
        GoldCoinUI coinScript = coin.GetComponent<GoldCoinUI>();

        if (coinScript != null)
        {
            coinScript.SetTarget(goldTarget, goldCoinSpeed);
        }
        else
        {
            Debug.LogError("GoldCoinUI script is missing on the prefab!");
        }
    }

    private void OnDestroy()
    {
        SpawnGoldCoin();
        enemiesLeft--;

        if (enemiesLeft <= 0 && spawner != null)
        {
            spawner.StartNextWave();
        }

        UpdateEnemiesLeftUI();
    }

    private void UpdateEnemiesLeftUI()
    {
        if (enemiesLeftText != null)
        {
            enemiesLeftText.text = $"Enemies Left: {enemiesLeft}";
        }
    }
}
