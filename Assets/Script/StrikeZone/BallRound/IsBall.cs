using UnityEngine;

//공의 스트.볼 여부 판단

public class IsBall : MonoBehaviour
{
    public GameObject[] Board;
    BallCountAway bsout;
    BallCountHome bspit;

    private void Start()
    {
        bsout = Board[0].GetComponent<BallCountAway>();
        bspit = Board[1].GetComponent<BallCountHome>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            BallState state = other.GetComponent<BallState>(); //공의 정보를 가져와서
            HitedBallState obj = other.GetComponent<HitedBallState>();


            if (state != null && state.passedStrikeZone && obj.obj == "Pitching") //공이 볼/스트 존에 닿았고, 스트라이크고 공이 던지는 중이면
            {
                Refree_test.Instance.judge(true); //스트일 경우만 심판 애니메이션 실행
                bsout.Strike_Count++; //스트라이크 카운트 ++
                bspit.Strike_Count++; //스트라이크 카운트 ++

                //SeeBallStateUI.Instance.NoticeStrike.SetActive(true);
                StartCoroutine(SeeBallStateUI.Instance.SeeGameObject(SeeBallStateUI.Instance.NoticeStrike, 2f));
                
            }
            else if (state != null && !state.passedStrikeZone && obj.obj == "Pitching") //볼 이라면
            {
                bsout.Ball_Count++; //볼카운트 ++
                bspit.Ball_Count++; //볼카운트 ++

                //SeeBallStateUI.Instance.NoticeBall.SetActive(true);
                StartCoroutine(SeeBallStateUI.Instance.SeeGameObject(SeeBallStateUI.Instance.NoticeBall, 2f));
            }

            Catching_Test.Instance.Throw_Ball(true); //스트, 볼 여부와 상관 없이 포수 애니메이션 실행
        }
    }
}
