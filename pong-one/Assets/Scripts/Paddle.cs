using UnityEngine;


/// <summary>
/// Paddle class which handles paddle movement
/// </summary>
public class Paddle : MonoBehaviour
{
    public float moveSpeed = 10f;
    public string edgesTag = "Edges";

    // Get input and move paddle
    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");

        MovePaddle(verticalInput);
    }

    // Movement handler
    public void MovePaddle(float direction)
    {
        float moveAmount = direction * moveSpeed * Time.deltaTime;

        // Calculate target position
        Vector3 targetPosition = transform.position + new Vector3(0, moveAmount, 0);

        // Move the paddle if not hitting an edge
        transform.Translate(0, moveAmount, 0);

        // Collision check to prevent passing through edges
        if (direction > 0)
        {
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f);

            foreach (Collider2D collider in hitColliders)
            {
                if (collider.gameObject.CompareTag(edgesTag) && collider.gameObject.name == "TopEdge")
                {
                    float edgeY = collider.transform.position.y;
                    float paddleHeight = GetComponent<BoxCollider2D>().size.y;

                    float maxYPosition = edgeY - paddleHeight / 2;
                    transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, transform.position.y, maxYPosition), transform.position.z);
                }
            }
        }
        else if (direction < 0)
        {
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f);

            foreach (Collider2D collider in hitColliders)
            {
                if (collider.gameObject.CompareTag(edgesTag) && collider.gameObject.name == "BottomEdge")
                {
                    float edgeY = collider.transform.position.y;
                    float paddleHeight = GetComponent<BoxCollider2D>().size.y;

                    float minYPosition = edgeY + paddleHeight / 2;
                    transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minYPosition, transform.position.y), transform.position.z);
                }
            }
        }
    }
}
