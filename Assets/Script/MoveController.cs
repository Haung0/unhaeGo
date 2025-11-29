using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float runSpeed = 9f;
   

    [Header("총알 공격 설정")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public int maxBullets = 6;
    public float shootCooldown = 2f;
    public int bulletDamage = 7;
    private int bulletsLeft;
    private float nextShootTime = 0f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded = true;

    private SpriteRenderer spriteRenderer;
    private Animator Anim;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletsLeft = maxBullets;
    }

    void Update()
    {
        // 좌우 이동
        float moveX = Input.GetAxisRaw("Horizontal");
        moveInput = new Vector2(moveX, 0).normalized;
        Anim.SetBool("isWalking", moveX != 0);

        // 점프
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // 스프라이트 Flip
        if (moveInput.x > 0) spriteRenderer.flipX = false;
        else if (moveInput.x < 0) spriteRenderer.flipX = true;

        // 총알 발사 (우클릭)
        if (Input.GetMouseButtonDown(1) && Time.time >= nextShootTime)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        rb.velocity = new Vector2(moveInput.x * currentSpeed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // 총알 발사
    void Shoot()
    {
        if (bulletsLeft <= 0)
        {
            nextShootTime = Time.time + shootCooldown;
            bulletsLeft = maxBullets;
            Debug.Log("재장전 완료!");
            return;
        }

        // 마우스 방향 계산
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - bulletSpawnPoint.position);
        direction.Normalize();

        // 총알 생성
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Bullet 스크립트에 방향 전달
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = bulletDamage;
            bulletScript.SetDirection(direction);  // ← 여기서 방향 전달!
        }

        bulletsLeft--;
        nextShootTime = Time.time + 0.2f;
    }

}
