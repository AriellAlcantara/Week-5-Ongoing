using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2f;
    public bool useCubic = false;

    public GameObject goldCoinPrefab; // Assign in Inspector
    public Transform goldTarget;      // Assign in Inspector
    public float goldCoinSpeed = 7f;  // Adjustable speed for coin movement

    private float t = 0f;
    private Vector3 startPos;

    // Track active enemies
    public static int enemiesLeft = 0;

    void Start()
    {
        target = GameObject.Find("TargetPoint")?.transform;
        goldTarget = GameObject.Find("GoldTarget")?.transform;

        if (target == null)
        {
            Debug.LogError("TargetPoint GameObject not found!");
        }
        if (goldTarget == null)
        {
            Debug.LogError("GoldTarget GameObject not found!");
        }

        startPos = transform.position;
    }

    void Update()
    {
        if (target == null) return;

        t += Time.deltaTime * moveSpeed;
        transform.position = useCubic ? CubicLerp(startPos, target.position, t) : QuadraticLerp(startPos, target.position, t);

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

    Vector3 CubicLerp(Vector3 start, Vector3 end, float t)
    {
        return Vector3.Lerp(start, end, t * t * t);
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

        if (enemiesLeft <= 0)
        {
            Spawner spawner = FindFirstObjectByType<Spawner>();
            if (spawner != null)
            {
                spawner.StartNextWave(); // No arguments needed
            }
        }
    }
}
