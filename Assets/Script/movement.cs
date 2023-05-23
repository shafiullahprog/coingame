using UnityEngine;

public class movement : MonoBehaviour
{
    public float laneWidth = 2.0f; 
    public int startingLane = 2;
    public bool isShiledOn;

    private int currentLane;
    private bool isMoving;
    private Vector3 targetPosition;
   
    [SerializeField] GameManager gameManager;    
    [SerializeField] float timer = 0;
    [SerializeField] int shieldOffTimer;
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
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 10.0f);
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveToLane(currentLane - 1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveToLane(currentLane + 1);
            }
        }

        Timer();
    }

    void Timer()
    {
        if (isShiledOn)
        {
            this.transform.GetComponent<SpriteRenderer>().color = Color.green;
            timer += Time.deltaTime;
            int seconds = (int)(timer % 60);

            if(seconds>= shieldOffTimer)
            {
                isShiledOn = false;
            }
        }
        else
        {
            timer = 0;
            this.transform.GetComponent<SpriteRenderer>().color = Color.white;
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
            if (!isShiledOn)
            {
                gameManager.GameOver();
                gameManager.gameOverScreen.SetActive(true);
            }
        }
        else if (collision.gameObject.CompareTag("coins"))
        {
            gameManager.coins++;
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("shield"))
        {
            collision.gameObject.SetActive(false);
            isShiledOn = true;
        }
    }
}
