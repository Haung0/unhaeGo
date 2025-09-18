using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20; // 회복량

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BaseController player = collision.GetComponent<BaseController>();
            if (player != null)
            {
                player.Heal(healAmount); // 플레이어 체력 회복
                Debug.Log("체력 회복: " + healAmount);
                Destroy(gameObject); // 아이템 제거
            }
        }
    }
}
