using UnityEngine;
using System.Collections;

//루 상의 주자 정보

public class RunnerController : MonoBehaviour
{
    Animator animator;
    private bool isMoving = false;

    public System.Action OnReachedFinalBase;

    [SerializeField] private float moveSpeed = 5f; //이동 속도
    [SerializeField] private float stoppingDistance = 0.1f; //베이스를 넘어가버리는 것을 방지

    public int Loc_Base = 0; //홈에서 출발

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void RunToBase(int BaseStart, int BaseEnd, Transform[] basePositions) //루 이동
    {
        if (!isMoving) //움직이고 있지 않다
            StartCoroutine(RunThroughBases(BaseStart, BaseEnd, basePositions)); //움직임
    }

    private IEnumerator RunThroughBases(int BaseStart, int BaseEnd, Transform[] basePositions) //주자 이동
    {
        isMoving = true;
        animator.SetBool("isRunning", true);

        for(int i=BaseStart;i<BaseEnd+1;i++) //현재 위치부터 이동 위치까지
        {
            Vector3 target = basePositions[i].position; //일단 다음 루로 이동
            while (Vector3.Distance(transform.position, target) > stoppingDistance) //베이스에 도착 했는가
            {
                transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime); //이동

                Vector3 direction = (target - transform.position).normalized; //다음 이동 벡터
                direction.y = 0;
                if (direction.sqrMagnitude > 0.001f) //다음 이동할 베이스를 바라봄
                {
                    Quaternion toRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
                }

                yield return null;
            }
            transform.position = target; //이동 완료
            yield return new WaitForSeconds(0.1f); //애니메이션 정리 시간
        }

        animator.SetBool("isRunning", false);
        isMoving = false;
        Loc_Base = BaseEnd + 1;

        if (BaseEnd == 3 && OnReachedFinalBase != null) //홈에 도착 완료
        {
            OnReachedFinalBase.Invoke(); //주자 삭제
        }
    }
}
