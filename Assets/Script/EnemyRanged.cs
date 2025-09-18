using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    [Header("총알 관련")]
    public GameObject bulletPrefab;
    public float fireRate = 2f;
    public float bulletSpeed = 5f;
    public int damage = 5;

    //[Header("체력 관련")]
    private float MaxHp = 20f;
    private float CurHp;
    private bool isDead = false;

    private Transform player;

    void Start()
    {
        //CurHp = MaxHp;
        player = GameObject.FindWithTag("Player").transform;
        InvokeRepeating(nameof(Fire), 1f, fireRate);
    }

    void Fire()
    {
        if (player == null || isDead) return;

        Vector2 dir = (player.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = dir * bulletSpeed;

        bullet.GetComponent<Bullet>().damage = damage;
    }

   
    public void TakeDamage(int amount)
    {
        if (isDead) return;

        CurHp -= amount;
        Debug.Log(gameObject.name + " 데미지: " + amount);

        if (CurHp <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        CurHp = 0;
        Debug.Log(gameObject.name + " 사망!");

        // 필요하면 총알 발사 중지
        CancelInvoke(nameof(Fire));

        // 2초 후 제거
        Destroy(gameObject, 2f);
    }
}
