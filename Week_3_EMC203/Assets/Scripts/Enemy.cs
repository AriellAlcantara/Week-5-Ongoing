using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2f;
    public bool useCubic = false;

    private float t = 0f;
    private Vector3 startPos;

    void Start()
    {
        // Find the GameObject named "TargetPoint" and assign its transform to target
        target = GameObject.Find("TargetPoint").transform;

        // If you want to make sure the target is valid
        if (target == null)
        {
            Debug.LogError("TargetPoint GameObject not found!");
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
}
