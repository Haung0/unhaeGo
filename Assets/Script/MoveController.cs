using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float runSpeed = 9f;

    [Header("근접 공격 설정")]
    public int meleeDamage = 10;
    public float meleeRange = 1f;
    public LayerMask enemyLayer;
    public Transform attackPoint;

    [Header("총알 공격 설정")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public int maxBullets = 6;
    public float shootCooldown = 2f;
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

        // 근접 공격 (좌클릭)
        if (Input.GetMouseButtonDown(0))
        {
            MeleeAttack();
        }

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

    // 근접 공격
    void MeleeAttack()
    {
        //Anim.SetTrigger("isAttacking");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, meleeRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            BaseController bc = enemy.GetComponent<BaseController>();
            if (bc != null)
            {
                bc.TakeDamage(meleeDamage);
                Debug.Log("근접 공격! 데미지: " + meleeDamage);
            }
        }
    }

    // 총알 발사
    void Shoot()
    {
        if (bulletsLeft <= 0)
        {
            // 재장전
            nextShootTime = Time.time + shootCooldown;
            bulletsLeft = maxBullets;
            Debug.Log("재장전 완료!");
            return;
        }

        Vector3 direction = spriteRenderer.flipX ? Vector3.left : Vector3.right;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * 10f;

        // 총알 데미지 전달
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = meleeDamage; // 총알 데미지 = 근접 데미지와 동일 (원하면 따로 값 줄 수 있음)
        }

        bulletsLeft--;
        nextShootTime = Time.time + 0.2f;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, meleeRange);
    }
}
