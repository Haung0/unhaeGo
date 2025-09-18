using UnityEngine;
using UnityEngine.SceneManagement;

public class StageExit : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어만 감지
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
