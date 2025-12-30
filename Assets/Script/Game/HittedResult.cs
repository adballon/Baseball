using UnityEngine;

//공의 안타 홈련 여부 반환

public class HittedResult : MonoBehaviour
{
    public static HittedResult Instance;
    public int status;

    public BallCountHome Pitch; //투수가 보는 점수판
    public BallCountAway Outside; //타자가 보는 점수판

    private void Awake()
    {
        Instance = this;
    }

    private void Update() //현재 공격팀이 누구인가
    {
        if (GameState.Instance.status.Contains("Home")) //원정팀 공격
        {
            status = 0;
        }
        else if (GameState.Instance.status.Contains("Away")) //우리팀 공격
        {
            status = 1;
        }
    }
    public void hit() //안타
    {
        if (status == 0)
        {
            ScoreBoard.Instance.HitHomeInt++;
        }
        else
        {
            ScoreBoard.Instance.HitAwayInt++;
        }
        init_CountBoard();
    }

    public void HomeRun() //홈런
    {
        if (status == 0)
        {
            ScoreBoard.Instance.RunHomeInt++;
        }
        else
        {
            ScoreBoard.Instance.RunAwayInt++;
        }
        init_CountBoard();
    }

    public void init_CountBoard()
    {
        Pitch.Strike_Count = -1;
        Pitch.Ball_Count = -1;

        Outside.Strike_Count = -1;
        Outside.Ball_Count = -1;
    }
}
