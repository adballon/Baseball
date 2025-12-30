using UnityEngine;
using System.Collections.Generic;

//공 타격시에 공의 날아가는 궤적을 시각화

public class BallTrajectory : MonoBehaviour
{
    private LineRenderer line; //시각화를 위한 LineRenderer
    private List<Vector3> points = new List<Vector3>(); //선을 그리기 위한 궤적의 점을 저장
    private Rigidbody rb; //리지드 바디
    private bool isTracking = false; //시각화 여부
    HitedBallState status; //볼의 상태

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        status = GetComponent<HitedBallState>();

        line.positionCount = 0;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startWidth = 0.05f;
        line.endWidth = 0.01f;
        line.useWorldSpace = true;
    }

    void Update()
    {
        if (isTracking) //트래킹 시작
        {
            points.Add(transform.position); //한 프레임 마다 공의 위치를 저장
            line.positionCount = points.Count; //저장한 위치의 개수
            line.SetPositions(points.ToArray()); //저장한 배열을 바탕으로 시각화
        }

        if(status.obj != "Null") //공이 배트에 맞은 경우만 시각화
        {
            isTracking = false; //안 맞았다면 하지 않음
        }
    }

    public void StartTracking() //시각화 시작
    {
        points.Clear(); //배열 초기화
        isTracking = true; //시작
    }

    public void ClearTrajectory() //시각화 종료
    {
        isTracking = false; //종료
        points.Clear(); //배열 초기화
        line.positionCount = 0; //Renderer 초기화
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground")) //땅에 한 번 닿았다면
        {
            isTracking = false; //종료
        }
    }
}