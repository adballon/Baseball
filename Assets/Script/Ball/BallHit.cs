using System;
using UnityEngine;

//공이 배트에 맞은 후에 어떻게 날아갈까

public class BallHit : MonoBehaviour
{
    public GameObject batcontrol; //배트 정보
    private Rigidbody rb; //리지드 바디
    public HitedBallState state; //볼의 타격 결과 전송

    [SerializeField] private float restitution = 0.2f; //반발력
    [SerializeField] private float torqueMultiplier = 5f; //회전 힘
    [SerializeField] private SphereCollider sphereCollider; //콜라이더 값 가져옴

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        state = GetComponent<HitedBallState>();
    }

    public void hit()
    {
        GetComponent<BallTrajectory>()?.StartTracking(); //볼의 궤적 시각화 시작

        state.obj = "Null"; //배트에 맞은 상태
        var bat = batcontrol.GetComponent<BatController>(); //배트의 정보 가져옴
        if (bat == null) return;

        var pos = GetComponent<BallArea>(); //볼의 범위 가져옴

        float r = UnityEngine.Random.Range(0, 1f); //랜덤 값 생성

        Vector3 batVelocity = bat.Velocity; //배트의 이동 벡터 가져옴
        float batSpeed = batVelocity.magnitude; //이동 벡터를 바탕으로 배트의 속도 계산

        //공이 배트에 어디를 맞을 것인지 계산
        Vector3 center = pos.center; //볼의 중심
        float x = GaussianRange(pos.left, pos.right); //볼의 좌우범위 랜덤으로 가산
        float y = (r > 0.3f) ? GaussianRange(pos.down, center.y) : GaussianRange(center.y, pos.up); //랜덤 값을 바탕으로 공의 위.아래 부분 중 어디를 가져올 것인가
        float z = GaussianRange(pos.back, 0); //공이 얼마나 양 옆으로 비스듬하게 맞았냐
        Vector3 contactPoint = new Vector3(x, y, z); //지금까지 가져온 값을 바탕으로 공의 어디를 때릴 것인지 결정

        // 타구 방향 및 힘 계산
        //Vector3 hitDirection = new Vector3(200, 2, 200).normalized;
        Vector3 hitDirection = (center - contactPoint).normalized; //공을 던지는 위치와 타겟에 대한 방향벡터 계산
        hitDirection = transform.TransformDirection(hitDirection); //객체의 방향 기준을 월드 좌표계로 바꿈
        hitDirection = (hitDirection * 0.7f + batVelocity.normalized * 0.3f).normalized; //배트의 방향을 좀 더 가산

        hitDirection = (hitDirection + Vector3.up * (r / 2)).normalized; //공의 발사각도를 랜덤으로 가산

        Debug.Log(hitDirection);

        float hitPower = batSpeed * (1f + restitution); //배트스피드에 반발력 계산
        Vector3 spinAxis = Vector3.Cross(batVelocity, hitDirection).normalized; //공의 회전 축 계산
        float spinStrength = batSpeed * torqueMultiplier; //공의 회전 속도 계산

        rb.isKinematic = false; //공의 물리 계산
        rb.useGravity = true; //공의 중력 활성화

        rb.linearVelocity = Vector3.zero;

        rb.AddForce(hitDirection * hitPower, ForceMode.Impulse); //공의 방향으로 힘을 가함(공을 날림)
        rb.AddTorque(spinAxis * spinStrength, ForceMode.Impulse); //공의 회전 방향 및 세기를 가함
    }

    //랜덤함수, 랜덤값은 정규분포를 따른다
    float GaussianRange(float min, float max, float centerBias = 0.5f, float stdDev = 0.1f)
    {
        float Gaussian01()
        {
            float u1 = 1f - UnityEngine.Random.value;
            float u2 = 1f - UnityEngine.Random.value;
            float randStd = Mathf.Sqrt(-2f * Mathf.Log(u1)) * Mathf.Sin(2f * Mathf.PI * u2);
            return centerBias + stdDev * randStd;
        }

        float t;
        do
        {
            t = Gaussian01(); // 0~1 안에 들 때까지 반복
        } while (t < 0f || t > 1f);

        return Mathf.Lerp(min, max, t);
    }
}