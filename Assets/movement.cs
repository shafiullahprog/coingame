using UnityEngine;

public class movement : MonoBehaviour
{
    public float laneWidth = 2.0f; // Width of each lane
    public int startingLane = 2; // Initial lane of the player

    private int currentLane; // Current lane of the player
    private bool isMoving; // Flag to indicate if the player is currently moving

    private Vector3 targetPosition; // Target position of the player when moving
   
    [SerializeField] GameManager gameManager;
    private void Start()
    {
        currentLane = startingLane;
        UpdatePosition();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (isMoving)
        {
            // Move the player towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 10.0f);

            // Check if the player has reached the target position
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
        else
        {
            // Check for input to move the player
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveToLane(currentLane - 1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveToLane(currentLane + 1);
            }
        }
    }

    private void MoveToLane(int lane)
    {
        // Clamp the lane value within the range [1, 3]
        lane = Mathf.Clamp(lane, 1, 3);
        if (lane != currentLane)
        {
            currentLane = lane;
            UpdatePosition();
        }
    }
    private void UpdatePosition()
    {
        float targetX = (currentLane - 2) * laneWidth;
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        isMoving = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("obstacles"))
        {
            collision.gameObject.SetActive(false);

        }
        else if(collision.gameObject.CompareTag("coins"))
        {
            gameManager.coins++;
            collision.gameObject.SetActive(false);
        }
    }
}
