using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float moveSpeed = 5f;    // 좌우 이동 속도
    public float jumpForce = 7f;    // 점프 힘
    public float runSpeed = 9f;     // Shift 키 누를 때 이동 속도


    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded = true; // 바닥에 있는지 체크

    SpriteRenderer spriteRenderer;
    private Animator Anim;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 좌우 이동 입력
        float moveX = Input.GetAxisRaw("Horizontal");
        moveInput = new Vector2(moveX, 0).normalized;

        // ↑키를 눌렀고, 바닥에 있을 때만 점프 가능
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false; // 점프 후 공중에 있으므로 false로 설정해서 막음
        }

        //spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        if (moveInput.x > 0)        // 오른쪽 이동
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x < 0)   // 왼쪽 이동
        {
            spriteRenderer.flipX = true;
        }

    }

    void FixedUpdate()
    {
        // 좌우 이동
        //rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);

        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        rb.velocity = new Vector2(moveInput.x * currentSpeed, rb.velocity.y);

    }

    // 바닥 충돌 체크 (플랫폼이나 땅에 닿으면 isGrounded = true)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Tag가 "Ground"인 오브젝트와 충돌한 경우에만 바닥으로 간주
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

