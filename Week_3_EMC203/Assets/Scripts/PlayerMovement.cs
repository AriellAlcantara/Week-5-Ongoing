using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the player

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Reference the Rigidbody2D component
    }

    void Update()
    {
        // Get input for horizontal and vertical movement
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right arrow
        movement.y = Input.GetAxisRaw("Vertical");   // W/S or Up/Down arrow
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}