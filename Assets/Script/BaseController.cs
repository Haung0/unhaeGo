using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseController : MonoBehaviour
{
    [SerializeField]
    private Slider HPbar;

    [Header("체력 설정")]
    public float MaxHp = 30;
    public float CurHp = 30;

    [Header("UI 설정")]
    public GameObject deathTextUI; // "사망했습니다" 텍스트 오브젝트 연결

    private bool isDead = false;

    void Start()
    {
        HPbar.value = (float)CurHp / (float)MaxHp; // 초기화
        if (deathTextUI != null)
            deathTextUI.SetActive(false); // 시작 시 숨기기
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

    // 다른 오브제가 공격할 때 호출
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

        if (deathTextUI != null)
            deathTextUI.SetActive(true); // "사망했습니다" 문구 띄우기

        // 원하면 게임을 멈추게 할 수도 있음
        // Time.timeScale = 0f;
        Destroy(gameObject, 0.5f); // 2초 후 오브젝트 제거
    }

    public void Heal(int amount)
    {
        if (isDead) return; // 죽으면 회복 불가

        CurHp += amount;
        if (CurHp > MaxHp) CurHp = MaxHp;

        Debug.Log("체력 회복: " + amount + " 현재체력: " + CurHp);
    }
}
