using UnityEngine;

//스트라이크존 시각화

public class SeeStrikeZone : MonoBehaviour
{
    public float angleInDegrees = 45f; // 회전 각도
    public Vector3 Center = Vector3.zero; // 회전 중심
    public Vector3 axis = Vector3.forward; // 회전 축 (Z축 기준 회전)

    void OnDrawGizmos()
    {
        float halfWidth = 0.216f;
        float halfHeight = 0.7f;

        // 회전 정의
        Quaternion rotation = Quaternion.AngleAxis(angleInDegrees, axis);

        // 꼭짓점 정의 후 회전 적용
        Vector3 topLeft = rotation * new Vector3(-halfWidth, halfHeight, 0f) + Center;
        Vector3 topRight = rotation * new Vector3(halfWidth, halfHeight, 0f) + Center;
        Vector3 bottomRight = rotation * new Vector3(halfWidth, 0, 0f) + Center;
        Vector3 bottomLeft = rotation * new Vector3(-halfWidth, 0, 0f) + Center;

        // 그리기
        Gizmos.color = Color.green;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}