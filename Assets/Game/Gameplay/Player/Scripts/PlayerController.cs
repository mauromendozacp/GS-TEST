using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Main Configuration")]
    [SerializeField] private Rigidbody2D rigid = null;
    [SerializeField] private float speed = 0f;
    [SerializeField] private int maxLives = 0;
    [SerializeField] private float jumpForce = 0f;

    private float currentLives = 0;
    private bool moveLeft = false;
    private bool moveRight = false;

    private void Start()
    {
        currentLives = maxLives;
    }

    private void Update()
    {
        MovePlayer();
    }

    public void LeftButtonPointDown()
    {
        moveLeft = true;
    }

    public void LeftButtonPointUp()
    {
        moveLeft = false;
    }

    public void RightButtonPointDown()
    {
        moveRight = true;
    }

    public void RightButtonPointUp()
    {
        moveRight = false;
    }

    public void JumpPlayer()
    {
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void MovePlayer()
    {
        if (moveRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (moveLeft)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}
