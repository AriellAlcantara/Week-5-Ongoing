using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 200f;
    private Transform target;

    void Update()
    {
        if (target == null)
        {
            target = FindNewTarget(); // Find a new target if the current one is destroyed
            if (target == null) return; // If no targets remain, missile stops moving
        }

        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotateSpeed * Time.deltaTime);
        transform.rotation = rotation;
        transform.position += transform.right * speed * Time.deltaTime;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget; // Assigns the target from the turret
    }

    Transform FindNewTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }
        return closest;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            FindFirstObjectByType<GameManager>().AddGold(5);
            target = FindNewTarget(); // Get a new target if available
            if (target == null)
            {
                Destroy(gameObject); // Destroy missile if no targets are left
            }
        }
    }
}
