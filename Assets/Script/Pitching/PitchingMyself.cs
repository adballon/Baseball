using UnityEngine;
using System.Collections;

public class PitchingMyself : MonoBehaviour
{
    public static PitchingMyself Instance;

    public string type = "FourSeam";

    public Transform[] target;
    public float delay = 2f; // 공 던지고 나서 다음 던지기까지 대기 시간

    public bool canPitch = true; // 공을 던질 수 있는 상태인지

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (KeyBinding.IsPitchButtonDown && canPitch)
        {
            StartCoroutine(DelayedPitching());
        }
    }

    public IEnumerator DelayedPitching()
    {
        canPitch = false; // 던진 직후 비활성화

        // 실제 던지기 로직
        Rigging.Instance.target = Rigging.Instance.targetZoneCenter[UnityEngine.Random.Range(0, Rigging.Instance.targetZoneCenter.Length)];
        PitchType.Instance.target = SelectTarget.Instance.target;
        StartCoroutine(Batter_test.Instance.DelayedHit());

        if (PitchType.Instance.spawnball != null)
        {
            PitchType.Instance.spawnball.GetComponent<BallTrajectory>().ClearTrajectory();
            Destroy(PitchType.Instance.spawnball, 1f);
            PitchType.Instance.spawnball = null;
        }

        ThrowBall.Instance.throwBall(type);

        yield return new WaitForSeconds(delay); // 쿨타임 대기
        canPitch = true; // 다시 던질 수 있게
    }
}
