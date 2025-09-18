using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{
    [SerializeField]
    private Slider HPbar;

    public float MaxHp = 30;
    public float CurHp = 30;

    private bool isDead = false;

    void Start()
    {
        HPbar.value = (float)CurHp / (float)MaxHp; // 초기화
    }

    void Update()
    {
        UpdateHP();

        if (isDead) return;

        if (CurHp <= 0)
        {
            Die();
        }
    }

    private void UpdateHP()
    {
        HPbar.value = Mathf.Lerp(HPbar.value, (float)CurHp / (float)MaxHp, Time.deltaTime * 10);
    }

    // ?? 다른 오브제가 공격할 때 호출하는 함수
    public void TakeDamage(int amount)
    {
        if (isDead) return;

        CurHp -= amount;
        if (CurHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        CurHp = 0;


        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Animator>().enabled = false;

        Debug.Log("캐릭터 사망!");

    }

    public void Heal(int amount)
    {
        if (isDead) return; // 죽으면 회복 불가

        CurHp += amount;
        if (CurHp > MaxHp) CurHp = MaxHp;

        Debug.Log("체력 회복: " + amount + " 현재체력: " + CurHp);
    }

}
