using UnityEngine;
using UnityEngine.Splines;

//외야 펜스 만들기
public class OutFieldFence : MonoBehaviour
{
    [Header("Spline 연결")]
    public SplineContainer spline;

    [Header("펜스 프리팹")]
    public GameObject fencePrefab;

    [Header("펜스 높이 (중심 기준)")]
    public float fenceHeight = 3.0f;

    [Header("펜스 조각 길이")]
    public float fenceLength = 8.0f;

    void Start()
    {
        if (spline == null || fencePrefab == null)
        {
            Debug.LogError("Spline 또는 FencePrefab을 연결해 주세요!");
            return;
        }

        // Spline 전체 길이
        float arcLength = spline.CalculateLength();

        // 펜스 조각 몇 개 배치할지
        int segments = Mathf.RoundToInt(arcLength / fenceLength);

        Debug.Log($"arcLength = {arcLength}, segments = {segments}");

        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;

            // 곡선상 위치
            Vector3 pos = spline.EvaluatePosition(t);

            // 곡선의 방향
            Vector3 tangent = spline.EvaluateTangent(t);
            Quaternion rot = Quaternion.LookRotation(tangent, Vector3.up);

            // 펜스 중앙 높이에 맞게 Y축 올리기
            Vector3 fencePos = pos + new Vector3(0, fenceHeight / 2f, 0);

            Instantiate(fencePrefab, fencePos, rot, transform);
        }
    }
}
