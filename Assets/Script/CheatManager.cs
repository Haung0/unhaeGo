using UnityEngine;

public class CheatManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            // 老馆 利 力芭
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy e in enemies)
            {
                Destroy(e.gameObject);
            }

            // 盔芭府 利 力芭
            EnemyRanged[] rangedEnemies = FindObjectsOfType<EnemyRanged>();
            foreach (EnemyRanged re in rangedEnemies)
            {
                Destroy(re.gameObject);
            }

            // 焊胶 力芭
            Boss[] bosses = FindObjectsOfType<Boss>();
            foreach (Boss b in bosses)
            {
                Destroy(b.gameObject);
            }

            Debug.Log("葛电 利 力芭!");
        }
    }
}
