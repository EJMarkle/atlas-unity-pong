using UnityEngine;

/// <summary>
/// Ball class which governs movement and collisions
/// </summary>
public class Ball : MonoBehaviour
{
    public float initialForce = 5f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InitialMovement();
    }

    // Start moving the ball in 1 of 4 random directions, top-left, bottom-left, top-right, or bottom-right
    void InitialMovement()
    {
        Vector2[] directions = new Vector2[]
        {
            new Vector2(1, 1).normalized,
            new Vector2(1, -1).normalized,
            new Vector2(-1, 1).normalized,
            new Vector2(-1, -1).normalized
        };

        int randomIndex = Random.Range(0, directions.Length);
        Vector2 initialDirection = directions[randomIndex];

        rb.AddForce(initialDirection * initialForce, ForceMode2D.Impulse);
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Edges"))
        {
            HandleEdgeCollision();
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            HandlePaddleCollision(collision);
        }
    }

    void HandleEdgeCollision()
    {
        // Maintain horizontal velocity and reverse vertical angle
        Vector2 currentVelocity = rb.velocity;
        rb.velocity = new Vector2(currentVelocity.x, -currentVelocity.y);
    }

    void HandlePaddleCollision(Collider2D paddleCollider)
    {
        // Get the paddle's transform and size
        RectTransform paddleTransform = paddleCollider.GetComponent<RectTransform>();
        Vector2 paddleSize = paddleTransform.sizeDelta;

        // Calculate hit position on the paddle
        float hitPositionY = transform.position.y - paddleTransform.position.y;
        float normalizedHitPosition = hitPositionY / (paddleSize.y / 2f);

        // Calculate bounce angle based on hit position
        float bounceAngle = Mathf.Lerp(-45f, 45f, normalizedHitPosition);

        // Determine the direction of the reflection based on the side of the paddle hit
        float reflectionDirection = (rb.velocity.x < 0) ? 1.0f : -1.0f;
        Vector2 reflectionDirectionVector = new Vector2(reflectionDirection, Mathf.Sign(hitPositionY)).normalized;

        // Calculate the reflected velocity
        Vector2 newVelocity = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * reflectionDirectionVector;
        rb.velocity = newVelocity.normalized * rb.velocity.magnitude;
    }
}