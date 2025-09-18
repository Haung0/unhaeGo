using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("따라갈 대상")]
    public Transform target;      // 플레이어 Transform

    [Header("카메라 설정")]
    public float smoothSpeed = 0.125f; // 부드럽게 이동할 속도
    public Vector3 offset = new Vector3(0, 0, -10); // 카메라 위치 오프셋

    void LateUpdate()
    {
        if (target == null) return;

        // 목표 위치 = 플레이어 위치 + 오프셋
        Vector3 desiredPosition = target.position + offset;

        // 부드럽게 이동
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // z값은 카메라 기본값 유지
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, desiredPosition.z);
    }
}
