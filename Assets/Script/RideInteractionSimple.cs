using UnityEngine;

public class RideInteractionSimple : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite rideSprite;
    public float interactRange = 2f;
    public GameObject rideObject;
    private SpriteRenderer sr;
    private Animator animator;   // Animator 추가
    private bool isRiding = false;



    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();  // Animator 가져오기
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            float distance = Vector2.Distance(transform.position, rideObject.transform.position);
            if (distance <= interactRange)
            {
                if (!isRiding)
                {
                    if (animator != null) animator.enabled = false;  // Animator 끄기
                    sr.sprite = rideSprite;
                    rideObject.SetActive(false);
                    isRiding = true;
                }
                else
                {
                    sr.sprite = normalSprite;
                    if (animator != null) animator.enabled = true;   // Animator 다시 켜기
                    rideObject.SetActive(true);
                    isRiding = false;
                }
            }
        }
    }
}
