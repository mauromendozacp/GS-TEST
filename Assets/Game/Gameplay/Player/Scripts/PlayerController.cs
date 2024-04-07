using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Main Configuration")]
    [SerializeField] private Rigidbody2D rigid = null;
    [SerializeField] private BoxCollider2D boxCollider = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] SpriteRenderer sprite = null;
    [SerializeField] private int maxLives = 0;
    
    [Header("Movement Configuration")]
    [SerializeField] private float speed = 0f;
    [SerializeField] private float leftLimitX = 0f;
    [SerializeField] private float rightLimitX = 0f;

    [Header("Jump Configuration")]
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private LayerMask floorLayer = default;

    private float currentLives = 0;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool playerOnFloor = false;

    private float checkFloorDistance = 0f;

    private void Start()
    {
        currentLives = maxLives;

        checkFloorDistance = boxCollider.bounds.size.y / 2f + 0.05f;
    }

    private void Update()
    {
        MovePlayer();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        CheckPlayerOnFloor();
    }

    public void LeftButtonPointDown()
    {
        moveLeft = true;
        sprite.flipX = true;
    }

    public void LeftButtonPointUp()
    {
        moveLeft = false;
    }

    public void RightButtonPointDown()
    {
        moveRight = true;
        sprite.flipX = false;
    }

    public void RightButtonPointUp()
    {
        moveRight = false;
    }

    public void JumpPlayer()
    {
        if (playerOnFloor)
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void MovePlayer()
    {
        Vector2 movePosition = Vector3.zero;

        if (moveRight)
        {
            if (transform.position.x < rightLimitX)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }
        else if (moveLeft)
        {
            if (transform.position.x > leftLimitX)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }
    }

    private void CheckPlayerOnFloor()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, checkFloorDistance, floorLayer);

        playerOnFloor = hit.collider != null;
    }

    private void UpdateAnimation()
    {
        animator.SetBool("IsWalking", moveRight || moveLeft);
    }
}
