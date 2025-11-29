using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header("이동 및 공격 설정")]
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    public Transform player;

    public float attackRange = 1f;    // 공격 범위
    public int damage = 5;            // 공격력
    public float attackRate = 1f;     // 초당 공격 횟수

    
    private bool isDead = false;

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private float nextAttackTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //CurHp = MaxHp;
    }

    void Update()
    {
        if (player == null || isDead) return;

        MoveTowardsPlayer();
        TryAttack();
    }

    void MoveTowardsPlayer()
    {
        float directionX = player.position.x - transform.position.x;
        rb.velocity = new Vector2(Mathf.Sign(directionX) * moveSpeed, rb.velocity.y);

        // 점프 높이 증가
        if (isGrounded && Mathf.Abs(player.position.y - transform.position.y) > 1.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }

        // 플레이어 바라보기
        if (directionX != 0)
            transform.localScale = new Vector3(Mathf.Sign(directionX), 1, 1);
    }


    void TryAttack()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange && Time.time >= nextAttackTime)
        {
            //animator.SetTrigger("isAttacking");
            player.GetComponent<BaseController>().TakeDamage(damage);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}
