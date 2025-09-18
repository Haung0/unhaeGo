using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("보스 설정")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        InvokeRepeating(nameof(Fire), 2f, fireRate);
    }

    void Update()
    {

    }

    void Fire()
    {
        if (player == null) return;

        // 총알 생성
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = (player.position - firePoint.position).normalized;
        rb.velocity = dir * 5f;
    }
}
