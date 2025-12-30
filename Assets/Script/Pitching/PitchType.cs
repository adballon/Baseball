using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구종 구현

public class PitchType : MonoBehaviour
{
    public static PitchType Instance;

    public GameObject ballPrefab; //던질 공
    public Transform spawnPoint; // 손 위치 또는 spawn 기준
    public Vector3 target;     // 스트라이크 존 근처 타겟 (포수 위치)
    public float BallSpeed = 20f; //직구 기준 속도

    public float ChangeUpDropDelay = 0.5f; // 몇 초 후 중력 적용
    public float ChangeUpGravityScale = 0.5f; //중력을 얼마나 반영할 것인가

    public float curveMagnitude = 5f; // 커브의 강도
    public float curveDuration = 1f; // 커브가 적용되는 시간 (공이 던져진 후)

    public float DestroyTime = 2f; //공을 던진 후 삭제 시간

    public GameObject spawnball;

    private void Update()
    {
        target = SelectTarget.Instance.target; //공을 어디에 던질 것인가
    }
    void Awake()
    {
        Instance = this;
    }

    public void FourSeam() //포심
    {
        Quaternion rot = Quaternion.Euler(0, 45, 0);
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, rot); //공 생성
        spawnball = ball;
        Rigidbody rb = ball.GetComponent<Rigidbody>(); //리지드 바디
        rb.useGravity = false; //직선으로 날아가기 위한 중력 삭제

        Vector3 direction = (target - spawnPoint.position).normalized; //지정된 타켓과의 방향벡터 계산
        rb.linearVelocity = direction * BallSpeed; //속도
    }


    public void ChangeUp() //체인지업
    {
        Quaternion rot = Quaternion.Euler(0, 45, 0);
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, rot); //공 생성
        spawnball = ball;
        Rigidbody rb = ball.GetComponent<Rigidbody>(); //ball's rigidbody
        rb.useGravity = false; //직선으로 날아가기 위한 중력 삭제

        Vector3 direction = (target - spawnPoint.position).normalized; //지정된 타켓과의 방향벡터 계산
        float ChangeUpSpeed = BallSpeed * 0.8f;
        rb.linearVelocity = direction * ChangeUpSpeed; //속도

        StartCoroutine(ApplyCustomGravity(rb, ChangeUpDropDelay, ChangeUpGravityScale)); //일정 시간 지난 후에 중력 적용
    }

    private IEnumerator ApplyCustomGravity(Rigidbody rb, float delay, float gravityScale) //공의 중력 적용
    {
        yield return new WaitForSeconds(delay); //일정 시간이 지난 후

        while (rb != null)
        {
            // 원하는 강도로 중력 적용(공이 떨어짐)
            rb.AddForce(Vector3.down * 9.81f * gravityScale, ForceMode.Acceleration);
            yield return new WaitForFixedUpdate();
        }
    }

    public void CurveBall() //커브
    {
        Quaternion rot = Quaternion.Euler(0, 45, 0);
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, rot);
        spawnball = ball;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = true; //공의 중력 적용

        Vector3 direction = (target - spawnPoint.position).normalized;
        float speed = BallSpeed * 0.75f; //공의 속력

        var velo = direction * speed;

        float arcPeakY = Mathf.Abs(Mathf.Max(spawnPoint.position.y, target.y)) + 1.0f; //포물선의 최고 높이 설정

        Vector3 velocity = CalculateThrowVelocity(spawnPoint.position, target, arcPeakY); //포물선 계산
        rb.linearVelocity = velocity; //던짐
    }

    public static Vector3 CalculateThrowVelocity(Vector3 from, Vector3 to, float arcPeakY)
    {
        Vector3 displacement = to - from; //공의 방향 벡터 (크기 1 아님)
        Vector3 displacementXZ = new Vector3(displacement.x, 0f, displacement.z); //방향벡터의 높이 값 삭제
        float distanceXZ = displacementXZ.magnitude; //방향 벡터의 상수 값

        float gravity = Mathf.Abs(Physics.gravity.y); //중력 가속도

        if (arcPeakY <= from.y || arcPeakY <= to.y) //던지거나 받는 위치보다 포물선의 높이가 커야함
        {
            arcPeakY = Mathf.Max(from.y, to.y) + 0.5f; //그거보다는 크게
        }

        float h1 = arcPeakY - from.y; //시작점에서의 최고 높이간의 차이
        float h2 = arcPeakY - to.y; //종료지점에서의 최고 높이간의 차이

        float t_up = Mathf.Sqrt(2f * h1 / gravity); //상승 시간
        float t_down = Mathf.Sqrt(2f * h2 / gravity); //하강 시간
        float totalTime = t_up + t_down; //공이 날아가는 총 시간

        float vy = gravity * t_up; //초기 y방향 속도
        float vxz = distanceXZ / totalTime; //수평 방향 속도 크기

        Vector3 result = displacementXZ.normalized * vxz; //수평 속도 벡터
        result.y = vy; //초기 속도 벡터
        return result;
    }
}