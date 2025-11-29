using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType { Player, Enemy }
    public BulletType bulletType = BulletType.Player;

    public int damage = 5;
    public float speed = 10f;
    public float lifeTime = 3f;

    private Rigidbody2D rb;
    private Vector2 moveDirection; // 플레이어나 EnemyRanged에서 지정하는 방향

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    // 방향을 외부에서 설정
    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir.normalized;
    }

    void FixedUpdate()
    {
        // 방향으로 일자 이동
        rb.velocity = moveDirection * speed;
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
