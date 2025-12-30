using UnityEngine;
using System.Collections;
using Unity.XR.CoreUtils;

//공이 배트에 맞은 뒤의 상태 반환

public class HitedBallState : MonoBehaviour
{
    public string obj = "Pitching"; //공이 던져지는 상태

    public BallCountHome Pitch; //투수가 보는 점수판
    public BallCountAway Outside; //타자가 보는 점수판
    public BatterRunnerManager runner; //주자
    public float UICountTime = 2f;
    
    private void Start()
    {
        Pitch = GameObject.Find("CountBoard_Pitch").GetComponent<BallCountHome>();
        Outside = GameObject.Find("CountBoard_Outside").GetComponent<BallCountAway>();
        runner = GameObject.Find("RunnerController").GetComponent<BatterRunnerManager>();


    }

    private void OnCollisionEnter(Collision other)
    {
        if (obj == "Null") //볼이 배트에 맞고 어디 떨어지지 않음
        {
            obj = other.gameObject.tag; //떨어진 그 지점 표시
        }

        Debug.Log(other.gameObject.tag + " " + other.gameObject.name + " " + obj);

        switch (obj) //어디에 떨어졌냐
        {
            case "One_Base_Hit": //1루타 지점
                HittedResult.Instance.hit(); //안타
                runner.SpawnBatterAndRun(1); //1루 진출
                StartCoroutine(SeeBallStateUI.Instance.SeeGameObject(SeeBallStateUI.Instance.NoticeOne, UICountTime));
                init();
                break;
            case "Two_Base_Hit": //2루타 지점
                HittedResult.Instance.hit(); //안타
                runner.SpawnBatterAndRun(2); //2루 진출
                StartCoroutine(SeeBallStateUI.Instance.SeeGameObject(SeeBallStateUI.Instance.NoticeTwo, UICountTime));
                init();
                break;
            case "Fence": //3루타 지점(펜스에 맞음)
                HittedResult.Instance.hit(); //안타
                runner.SpawnBatterAndRun(3); //3루 진출
                StartCoroutine(SeeBallStateUI.Instance.SeeGameObject(SeeBallStateUI.Instance.NoticeThree, UICountTime));
                init();
                break;
            case "ScoreBoard": //외야 점수판(홈런)
                HittedResult.Instance.hit(); //안타
                runner.SpawnBatterAndRun(4); //홈런
                StartCoroutine(SeeBallStateUI.Instance.SeeGameObject(SeeBallStateUI.Instance.NoticeHomerun, UICountTime));
                init();
                break;
            case "Ground": //펜스 밖으로 나감(홈런)
                HittedResult.Instance.hit(); //안타
                runner.SpawnBatterAndRun(4); //홈런
                StartCoroutine(SeeBallStateUI.Instance.SeeGameObject(SeeBallStateUI.Instance.NoticeHomerun, UICountTime));
                init();
                break;
            case "Out": //아웃
                Pitch.Out_Count++; //투수 점수판 아웃 카운트 
                Outside.Out_Count++; //타자 점수판 아웃 카운트
                StartCoroutine(SeeBallStateUI.Instance.SeeGameObject(SeeBallStateUI.Instance.NoticeOut, UICountTime));
                HittedResult.Instance.init_CountBoard();
                init();
                break;
            case "Foul": //파울
                if (Pitch.Strike_Count < 1) //2 스트라이크 전이다
                {
                    Pitch.Strike_Count++; //투수 점수판 스트라이크 카운트
                    Outside.Strike_Count++; //타자 점수판 스트라이크 카운트
                }
                StartCoroutine(SeeBallStateUI.Instance.SeeGameObject(SeeBallStateUI.Instance.NoticeFoul, UICountTime));
                init();
                break;
            case "Untagged": //투수, 주자 등 오브젝트에 맞음(아웃)
                Pitch.Out_Count++;
                Outside.Out_Count++;
                StartCoroutine(SeeBallStateUI.Instance.SeeGameObject(SeeBallStateUI.Instance.NoticeOut, UICountTime));
                init();
                break;
            case "Spot": //배트에 맞아서 날아감
                obj = "Null";
                break;
            case "Skip": //스트라이크 존을 공이 지나감
                init();
                break;
        }
    }
    void init() //한 번 바뀐 상태는 다시 바뀌지 않음
    {
        obj = "Finish";
    }
}
