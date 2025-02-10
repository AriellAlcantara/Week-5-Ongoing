using UnityEngine;

public class GoldCoinUI : MonoBehaviour
{
    private Transform target;
    public float moveSpeed = 5f;

    private void Update()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform newTarget, float speed)
    {
        target = newTarget;
        moveSpeed = speed;
    }
}
