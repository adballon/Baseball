using System;
using System.Collections;
using UnityEngine;

//투수의 정보 반환

public class Pitching_test : MonoBehaviour
{
    Animator pitch;
    Rigidbody[] rb;

    public static Pitching_test Instance;

    public bool hasThrown = false; //공을 던지고 있는가
    public bool animationcontrol = true; //애니메이션 실행 여부
    public string type = "FourSeam"; //던질 구종 초기화

    public Transform RightHand; //오른손 잡이
    public Transform autoThrowPoint; //릴리즈 포인트
    public float autoThrowRadius = 0.31f; //포인트 범위

    public IEnumerator DelayedThrow()
    {
        yield return new WaitForSecondsRealtime(4f); // 공을 던지고 4초후 다시

        animationcontrol = true;
        hasThrown = false;
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponentsInChildren<Rigidbody>();
        pitch = GetComponentInChildren<Animator>();
        Kinetic(true);
    }

    void Update()
    {
        if (KeyBinding.IsPitchButtonDown) //신호를 주면
        {
            if (animationcontrol) //애니메이션 실행 중이 아니면
            {
                Rigging.Instance.target = Rigging.Instance.targetZoneCenter[UnityEngine.Random.Range(0, Rigging.Instance.targetZoneCenter.Length)];
                
                endThrow(true);  //애니메이션 실행
                animationcontrol = false; //애니메이션 실행 중 
            }
        }

        CheckAutoThrow();
    }

    void endThrow(bool b) //애니메이션 제어
    {
        pitch.SetBool("Throw", b);
    }

    void Kinetic(bool b) //물리상태 제어
    {
        foreach (var rigidchildren in rb)
        {
            rigidchildren.isKinematic = b;
        }
    }

    void LateUpdate() //투수의 애니메이션 종료 시점을 확인해서 idle로 돌림
    {
        AnimatorStateInfo info = pitch.GetCurrentAnimatorStateInfo(0); //투수의 애니메이션 정보를 가져옴

        if (info.IsName("Pitch") && info.normalizedTime >= 1.0f) //투수의 애니메이션이 끝남
        {
            pitch.SetBool("Throw", false); //애니메이션을 종료
        }
    }

    void CheckAutoThrow() //손이 릴리즈포인트에 도착함
    {
        if (autoThrowPoint == null)
        {
            return;
        }
        Transform target = autoThrowPoint;
        float distance = Vector3.Distance(RightHand.transform.position, target.position); //손과 포인트의 거리가

        if (distance < autoThrowRadius && !hasThrown) //범위 내로 들어오고 볼을 던지지 않음
        {
            ThrowingBall(); //볼을 던짐
            hasThrown = true; //볼을 던짐을 알림
        }
    }

    public void ThrowingBall() //공을 던짐
    {
        if (PitchType.Instance.spawnball != null)
        {
            PitchType.Instance.spawnball.GetComponent<BallTrajectory>().ClearTrajectory();
            Destroy(PitchType.Instance.spawnball, 1f);
            PitchType.Instance.spawnball = null;
        }

        type = ThrowBall.Instance.pitchtype[UnityEngine.Random.Range(0, ThrowBall.Instance.pitchtype.Length)];
        ThrowBall.Instance.throwBall(type); //공을 던짐
        StartCoroutine(Batter_test.Instance.DelayedHit()); //타자 이벤트도 같이 수행
        StartCoroutine(DelayedThrow());
    }
}
