using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("추적 설정")]
    public Transform player;          // 플레이어 Transform
    public float moveSpeed = 3f;      // 이동 속도
    public float followRange = 5f;    // 플레이어를 따라가는 범위

    [Header("체력 설정")]
    public int maxHP = 50;            // 최대 체력
    private int currentHP;            // 현재 체력

    void Start()
    {
        currentHP = maxHP;

        // 플레이어 자동 찾기
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= followRange)
            {
                FollowPlayer();
            }
        }
    }

    void FollowPlayer()
    {
        // 플레이어 방향 계산
        Vector3 direction = (player.position - transform.position).normalized;

        // 이동
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // 적 데미지 처리 함수
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("Damaged");
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        Destroy(gameObject); // 적 제거
        Debug.Log("Dead Enemy");
    }
}
