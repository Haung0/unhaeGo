using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f; // 1.5초마다 발사
    public float bulletSpeed = 7f;
    public int bulletDamage = 3;

    private float nextFireTime = 0f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        // 일정 시간마다 총알 발사
        if (Time.time >= nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootAtPlayer()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // 플레이어 방향 계산
        Vector2 direction = (player.position - firePoint.position).normalized;

        // 총알 생성
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Bullet 스크립트에 방향과 속도 설정
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.bulletType = Bullet.BulletType.Enemy;
            bulletScript.damage = bulletDamage;
            bulletScript.SetDirection(direction); // 직선 방향으로 발사
        }
    }
}
