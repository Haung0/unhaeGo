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
        player = GameObject.FindWithTag("Player")?.transform;
        InvokeRepeating(nameof(Fire), 1f, fireRate);
    }

    void Fire()
    {
        if (player == null || isDead) return;

        // 플레이어 방향 계산
        Vector2 dir = (player.position - transform.position).normalized;

        // 총알 생성
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Bullet 스크립트 가져와 설정
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.bulletType = Bullet.BulletType.Enemy; // 적용 총알임을 명시
            bulletScript.damage = damage;
            bulletScript.speed = bulletSpeed;
            bulletScript.SetDirection(dir); // Bullet.cs에서 방향 지정
        }
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

        CancelInvoke(nameof(Fire)); // 총알 발사 중단
        Destroy(gameObject, 2f);    // 2초 후 삭제
    }
}
