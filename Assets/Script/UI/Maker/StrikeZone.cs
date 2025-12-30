using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//스트라이크 존 생성

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class StrikeZoneMesh : MonoBehaviour
{
    float strikeZoneTop = 1.3f;
    float strikeZoneBottom = 0.6f;
    float length = 21.6f * 0.01f;

    void Start()
    {
        // 중심 0,0,0 기준으로 작성
        float height = strikeZoneTop - strikeZoneBottom;

        // 밑면 5각형
        Vector3 p1 = new Vector3(length, strikeZoneBottom, 0);
        Vector3 p2 = new Vector3(length, strikeZoneBottom, length);
        Vector3 p3 = new Vector3(-length, strikeZoneBottom, length);
        Vector3 p4 = new Vector3(-length, strikeZoneBottom, 0.0f);
        Vector3 p5 = new Vector3(0, strikeZoneBottom, -length);

        // 윗면 5각형
        Vector3 p1Top = p1 + Vector3.up * height;
        Vector3 p2Top = p2 + Vector3.up * height;
        Vector3 p3Top = p3 + Vector3.up * height;
        Vector3 p4Top = p4 + Vector3.up * height;
        Vector3 p5Top = p5 + Vector3.up * height;

        // 정점 배열
        Vector3[] vertices = new Vector3[]
        {
            p1, p2, p3, p4, p5,   // 0~4 (밑면)
            p1Top, p2Top, p3Top, p4Top, p5Top  // 5~9 (윗면)
        };

        // 삼각형 정의
        int[] triangles = new int[]
        {
            // 밑면
            0,1,2,  0,2,4,  4,2,3,
            // 윗면
            5,7,6,  5,9,7,  9,8,7,
            // 옆면 (벽)
            0,5,6, 0,6,1,
            1,6,7, 1,7,2,
            2,7,8, 2,8,3,
            3,8,9, 3,9,4,
            4,9,5, 4,5,0
        };

        Mesh mesh = new Mesh();
        mesh.name = "StrikeZoneMesh";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;

#if UNITY_EDITOR
        string path = "Assets/StrikeZoneMesh.asset";
        AssetDatabase.CreateAsset(Object.Instantiate(mesh), path);
        AssetDatabase.SaveAssets();
        Debug.Log($"메쉬를 {path} 에 저장했습니다.");
#endif
    }
}
