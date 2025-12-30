#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

//홈플레이트 제작

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HomePlate : MonoBehaviour
{
    [ContextMenu("Generate and Save HomePlate Mesh")]
    void GenerateAndSaveMesh()
    {
        float topWidth = 43.2f * 0.01f / 2f;
        Vector3 center = Vector3.zero;

        // XZ 평면 기준으로 정점 위치 정의 (Y=0)
        Vector3 p1 = center + new Vector3(-topWidth, 0, topWidth);  // 좌상단
        Vector3 p2 = center + new Vector3(topWidth, 0, topWidth);   // 우상단
        Vector3 p3 = center + new Vector3(topWidth, 0, 0f);         // 오른쪽 중간
        Vector3 p4 = center + new Vector3(0f, 0f, -topWidth);       // 아래 뾰족한 점
        Vector3 p5 = center + new Vector3(-topWidth, 0, 0f);        // 왼쪽 중간

        Vector3[] vertices = new Vector3[]
        {
            p1, p2, p3, p4, p5
        };

        int[] triangles = new int[]
        {
            0, 1, 4,
            1, 2, 4,
            2, 3, 4
        };

        Mesh mesh = new Mesh
        {
            name = "HomePlateMesh"
        };
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        // 저장 경로
        string folderPath = "Assets/Generated";
        string assetPath = folderPath + "/HomePlateMesh.asset";

        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder("Assets", "Generated");

        AssetDatabase.CreateAsset(mesh, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // 메시 필터에 적용
        GetComponent<MeshFilter>().mesh = mesh;

        Debug.Log("✅ 홈플레이트 메시 생성 및 저장 완료: " + assetPath);
    }
}
#endif
