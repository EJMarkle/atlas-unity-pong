using UnityEngine;


/// <summary>
/// Player class which handles user input and calls appropriate movement methods
/// </summary>
public class Player : MonoBehaviour
{
    public KeyCode upKey;
    public KeyCode downKey;

    private Paddle paddle;

    // Init paddle
    void Start()
    {
        paddle = GetComponent<Paddle>();
    }
    
    
    // Checks for user input
    void Update()
    {
        if (Input.GetKey(upKey))
        {
            MovePaddle(1);
        }
        else if (Input.GetKey(downKey))
        {
            MovePaddle(-1);
        }
    }

    // Move paddle in appropriate direction
    public void MovePaddle(int direction)
    {
        paddle.MovePaddle(direction);
    }
}
