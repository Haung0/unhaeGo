using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 5;
    public float lifeTime = 3f;
    public float speed = 10f;
    public float rotateSpeed = 200f; // 회전 속도 (유도 정도)

    private Rigidbody2D rb;
    private Transform target;

    // 총알 타입: 플레이어용인지 적용인지
    public enum BulletType { Player, Enemy }
    public BulletType bulletType = BulletType.Player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 목표 설정
        switch (bulletType)
        {
            case BulletType.Player:
                GameObject enemy = GameObject.FindWithTag("Enemy");
                if (enemy != null) target = enemy.transform;
                break;
            case BulletType.Enemy:
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null) target = player.transform;
                break;
        }

        Destroy(gameObject, lifeTime); // 수명 종료 시 삭제
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            rb.velocity = transform.right * speed; // 목표 없으면 직선
            return;
        }

        // 방향 계산
        Vector2 direction = (Vector2)(target.position - transform.position);
        direction.Normalize();

        // 현재 회전 각도
        float rotateAmount = Vector3.Cross(direction, transform.right).z;

        // 회전
        rb.angularVelocity = -rotateAmount * rotateSpeed;

        // 앞으로 이동
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (bulletType)
        {
            case BulletType.Player:
                if (other.CompareTag("Enemy"))
                {
                    Enemy enemy = other.GetComponent<Enemy>();
                    if (enemy != null)
                        enemy.TakeDamage(damage);

                    Destroy(gameObject);
                }
                break;

            case BulletType.Enemy:
                if (other.CompareTag("Player"))
                {
                    BaseController player = other.GetComponent<BaseController>();
                    if (player != null)
                        player.TakeDamage(damage);

                    Destroy(gameObject);
                }
                break;
        }
    }
}
