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

    //here
    private bool isDead = false;

    void Start()
    {
        HPbar.value = (float)CurHp / (float)MaxHp; //hp 초기화
    }
    void Update()
    {
        UpdateHP();

        //here
        if (isDead) return; // 죽었으면 조작/업데이트 중단

        if (CurHp <= 0)
        {
            Die();
        }
    }
    private void UpdateHP()
    {
        HPbar.value = Mathf.Lerp(HPbar.value, (float)CurHp / (float)MaxHp, Time.deltaTime * 10);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Enemy Tag를 가진 오브젝트와 충돌했을 시
        if (other.gameObject.CompareTag("Enemy"))
        {
            CurHp -= 1;
        }

    }

    //here
    private void Die()
    {
        isDead = true;
        CurHp = 0;

        Debug.Log("캐릭터 사망!");

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().simulated = false;
    }
}