using UnityEngine;

//타자의 타석 생성

public class BatterBoxes : MonoBehaviour
{
    void Start()
    {
        CreateBatterBox(
            "LeftBatterBox",
            new Vector3(-1.8f, 0.01f, 0.9f),
            new Vector3(-0.6f, 0.01f, 0.9f),
            new Vector3(-0.6f, 0.01f, -0.9f),
            new Vector3(-1.8f, 0.01f, -0.9f)
        );

        CreateBatterBox(
            "RightBatterBox",
            new Vector3(0.6f, 0.01f, 0.9f),
            new Vector3(1.8f, 0.01f, 0.9f),
            new Vector3(1.8f, 0.01f, -0.9f),
            new Vector3(0.6f, 0.01f, -0.9f)
        );
    }

    void CreateBatterBox(string name, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        GameObject lineObj = new GameObject(name);
        var lr = lineObj.AddComponent<LineRenderer>();

        lr.positionCount = 5;     // 4개의 꼭짓점 + 처음점으로 돌아오기
        lr.loop = true;
        lr.widthMultiplier = 0.02f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.endColor = Color.white;

        Vector3[] positions = new Vector3[]
        {
            p1, p2, p3, p4, p1
        };
        lr.SetPositions(positions);
    }
}
