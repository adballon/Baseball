using UnityEngine;
using System.Collections;

public enum StrikePos { RT, CT, LT, RC, CC, LC, RB, CB, LB }

//게임 내의 타자 정보
public class Batter_test : MonoBehaviour
{
    public static Batter_test Instance;
    Rigidbody[] rb; //리지드 바디
    Animator animator; //타자의 애니메이션
    public float delayBeforeHit = 0.1f;
    public float moveSpeed = 3f;
    public string type;

    Vector3[] batterPos =
    {
        new Vector3(99.164f, 0, 100.871f),
        new Vector3(99.242f, 0, 100.765f),
        new Vector3(99.32f, 0, 100.68f),
        new Vector3(99.248f, 0, 100.76f),
        new Vector3(99.36f, 0, 100.63f),
        new Vector3(99.43f, 0, 100.55f),
        new Vector3(99.267f, 0, 100.638f),
        new Vector3(99.408f, 0, 100.58f),
        new Vector3(99.49f, 0, 100.48f),
    };

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponentsInChildren<Rigidbody>();
        Kinetic(true); //타자의 물리 법칙을 끔

        transform.rotation = Quaternion.Euler(0, 140, 0);
    }

    void Update()
    {
        string cubeName = Rigging.Instance.target.name;
        StrikePos sp = (StrikePos)System.Enum.Parse(typeof(StrikePos), cubeName);
        Vector3 targetPos = batterPos[(int)sp];
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
        char c = returnloc(sp);

        if (Pitching_test.Instance != null && Pitching_test.Instance.gameObject.activeInHierarchy)
        {
            type = Pitching_test.Instance.type;
        }
        else if (PitchingMyself.Instance != null && PitchingMyself.Instance.gameObject.activeInHierarchy)
        {
            type = PitchingMyself.Instance.type;
        }

        if (type == "FourSeam")
        {
            
            if (c == 'T') { delayBeforeHit = 0.685f;}
            else if (c == 'C' || c == 'B') { delayBeforeHit = 0.68f;}
        }
        else if (type == "ChangeUp")
        {
            
            if (c == 'T' || c == 'C' || c == 'B') { delayBeforeHit = 1.035f;}
        }
        else if (type == "CurveBall")
        {
            
            if (c == 'T' || c == 'C') { delayBeforeHit = 0.41f;}
            else if (c == 'B') { delayBeforeHit = 0.44f;}
        }

        if (KeyBinding.IsHitButtonDown) //신호를 주면 타자가 배트를 휘두름
        {
            Hit(true);
        }

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0); //현재의 애니메이션 정보를 가져옴

        if (info.IsName("Hit") && info.normalizedTime >= 1.0f) //애니메이션이 끝났다.
        {
            Hit(false); //애니메이션 비 활성화
        }
    }

    void Kinetic(bool b) //물리 법칙 사용 여부
    {
        foreach (var rigidchildren in rb)
        {
            rigidchildren.isKinematic = b;  // 초기에는 꺼놓음
        }
    }

    public void Hit(bool b) //애니메이션 제어
    {
        animator.SetBool("Hit", b);
    }

    public IEnumerator DelayedHit()
    {
        // 1. 딜레이
        yield return new WaitForSeconds(delayBeforeHit);

        // 2. 애니메이션 실행
        Hit(true);
    }

    char returnloc(StrikePos sp)
    {
        if (sp == StrikePos.LT || sp == StrikePos.CT || sp == StrikePos.RT)
        {
            return 'T';
        }
        else if (sp == StrikePos.LC || sp == StrikePos.CC || sp == StrikePos.RC)
        {
            return 'C';
        }
        else if (sp == StrikePos.LB || sp == StrikePos.CB || sp == StrikePos.RB)
        {
            return 'B';
        }

        return 'N';
    }
}
