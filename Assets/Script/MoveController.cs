using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float moveSpeed = 5f;    // 좌우 이동 속도
    public float jumpForce = 7f;    // 점프 힘

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded = true; // 바닥에 있는지 체크

    public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    void FixedUpdate()
    {
        // 좌우 이동
        //rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);

        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
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
