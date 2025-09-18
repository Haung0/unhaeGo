using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("공격 설정")]
    public float attackRange = 2f;         // 공격 범위
    public int damage = 5;                 // 공격력
    public float attackCooldown = 0.5f;    // 공격 딜레이
    public LayerMask enemyLayer;           // 적 Layer

    private float lastAttackTime;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= lastAttackTime + attackCooldown)
        {
            MeleeAttack();
            lastAttackTime = Time.time;
        }
    }

    void MeleeAttack()
    {
        // 범위 내 적 찾기
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            // Enemy 처리
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null) enemy.TakeDamage(damage);

            // EnemyRanged 처리
            EnemyRanged enemyRanged = hit.GetComponent<EnemyRanged>();
            if (enemyRanged != null) enemyRanged.TakeDamage(damage);

        }

        Debug.Log("근접 공격 실행! 적 수: " + hits.Length);
    }

    // Scene 뷰에서 공격 범위 시각화
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
