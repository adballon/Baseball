using UnityEngine;

//공을 원하는 위치에 생성

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Vector3 spawnPoint;

    private GameObject currentBall;

    public void SpawnBall()
    {
        if (currentBall != null)
        {
            currentBall.GetComponent<BallTrajectory>().ClearTrajectory(); //기존의 던진 공의 궤적 지우기
            Destroy(currentBall); //기존의 공 지우기
        }

        Quaternion rotation = Quaternion.Euler(0, 45f, 0); //생성할 공 회전 값 지정
        currentBall = Instantiate(ballPrefab, spawnPoint, rotation); //공 생성

        Rigidbody rb = currentBall.GetComponent<Rigidbody>(); //공의 리지드바디 가져옴
        if (rb != null)
        {
            rb.useGravity = false; //공이 떨어지지 않게 중력 삭제
            rb.linearVelocity = Vector3.zero; //물체가 움직이고 있지 않아 0
        }
    }

    private void Update()
    {
        if (KeyBinding.SpawnBall) //공 생성 신호를 주면
        {
            SpawnBall(); //공 생성
        }

        if (currentBall != null && currentBall.GetComponent<HitedBallState>().obj == "Finish")
        {
            currentBall.GetComponent<BallTrajectory>().ClearTrajectory();
            Destroy(currentBall, 2f);
            currentBall = null;
        }
    }
}
