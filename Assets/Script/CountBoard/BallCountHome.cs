using Unity.VisualScripting;
using UnityEngine;

public class BallCountHome : MonoBehaviour
{
    public BatterRunnerManager runner; //주자

    //볼카운트의 시각화 오브젝트
    public GameObject[] Strike; //스크라이크 오브젝트 배열
    public GameObject[] Ball; // 볼 오브젝트 배열
    public GameObject[] Out; //아웃 오브젝트 배열
    
    //각 볼카운트는 -1로 초기화
    public int Strike_Count = -1;
    public int Ball_Count = -1;
    public int Out_Count = -1;

    public bool isThreeOut = false;

    //시작 시 각 오브젝트 하얀색으로 초기화
    void Start()
    {
        runner = GameObject.Find("RunnerController").GetComponent<BatterRunnerManager>();
        ClearStrike();
        ClearBall();
        ClearOut();
    }

    void Update()
    {
        if(Strike_Count >= 2) //스트라이크가 3개다
        {
            Strike_Count = -1; //스트라이크 초기화
            Ball_Count = -1; // 볼 초기화
            Out_Count++; //아웃카운트 추가

            ClearStrike(); //스트라이크 오브젝트 초기화
            ClearBall(); //볼 오브젝트 초기화
        }

        if (Ball_Count >= 3) //볼이 4개다
        {
            Ball_Count = -1; //볼 초기화
            Strike_Count = -1; //스트라이크 초기화

            ClearBall(); //볼 오브젝트 초기화
            ClearStrike(); //스트라이크 오브젝트 초기화

            if (HittedResult.Instance.status == 0)
            {
                ScoreBoard.Instance.BaseOnBallHomeInt++;
                runner.SpawnBatterAndRun(1);
            }
        }

        if (Out_Count >= 2) //아웃이 3개다
        {
            isThreeOut = true;

            Out_Count = -1; //아웃 초기화
            Strike_Count = -1; //스트라이크 초기화
            Ball_Count = -1; //볼 초기화

            ClearStrike(); //스트라이크 오브젝트 초기화
            ClearBall(); //볼 오브젝트 초기화
            ClearOut(); //아웃 오브젝트 초기화
            PitchingMyself.Instance.canPitch = true;
            Pitching_test.Instance.animationcontrol = true;
            Pitching_test.Instance.hasThrown = false;
        }

        //각 카운트에 맞게 오브젝트 시각화
        CountStrike(new Color(1f, 0.5f, 0f));
        CountBall(Color.green);
        CountOut(Color.red);
        
    }

    void CountStrike(Color c)
    {
        if (Strike_Count >= 0 && Strike_Count < 2) //스트라이크가 1~2?
        {
            Renderer r = Strike[Strike_Count].GetComponent<Renderer>();
            r.material.color = c; //그 카운트에 맞게 색칠
        }

        if (Strike_Count < 0)
        {
            ClearStrike();
        }
    }

    void CountBall(Color c)
    {
        if (Ball_Count >= 0 && Ball_Count < 3) //볼이 1~3?
        {
            Renderer r = Ball[Ball_Count].GetComponent<Renderer>();
            r.material.color = c; //그 카운트에 맞게 색칠
        }

        if (Ball_Count < 0)
        {
            ClearBall();
        }
    }

    void CountOut(Color c)
    {
        if (Out_Count >= 0 && Out_Count < 2) //아웃이 1~2?
        {
            Renderer r = Out[Out_Count].GetComponent<Renderer>();
            r.material.color = c; //그 카운트에 맞게 색칠
        }
    }

    void ClearStrike() //스트라이크 오브젝트 초기화
    {
        foreach(var obj in Strike)
        {
            Renderer r = obj.GetComponent<Renderer>();
            r.material.color = Color.white;
        }
    }
    void ClearBall() //볼 오브젝트 초기화
    {
        foreach (var obj in Ball)
        {
            Renderer r = obj.GetComponent<Renderer>();
            r.material.color = Color.white;
        }
    }
    void ClearOut() //아웃 오브젝트 초기화
    {
        foreach (var obj in Out)
        {
            Renderer r = obj.GetComponent<Renderer>();
            r.material.color = Color.white;
        }
    }
}
