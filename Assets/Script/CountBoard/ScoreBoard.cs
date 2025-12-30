using TMPro;
using UnityEngine;

//점수판 업데이트
public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;

    [Header("Home Score")]
    public TextMeshProUGUI[] ScoreHome;
    public TextMeshProUGUI RunHome;
    public TextMeshProUGUI HitHome;
    public TextMeshProUGUI BaseOnBallHome;
    public int[] scoreHomeInt = { 0, 0, 0 };
    public int RunHomeInt = 0;
    public int HitHomeInt = 0;
    public int BaseOnBallHomeInt = 0;

    [Header("Away Score")]
    public TextMeshProUGUI[] ScoreAway;
    public TextMeshProUGUI RunAway;
    public TextMeshProUGUI HitAway;
    public TextMeshProUGUI BaseOnBallAway;
    public int[] scoreAwayInt = { 0, 0, 0 };
    public int RunAwayInt = 0;
    public int HitAwayInt = 0;
    public int BaseOnBallAwayInt = 0;

    void Start()
    {
        Score_Update();
    }

    void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        Score_Update();
    }

    void Score_Update()
    {
        for (int i = 0; i < ScoreHome.Length; i++)
        {
            ScoreHome[i].text = scoreHomeInt[i].ToString();
            ScoreAway[i].text = scoreAwayInt[i].ToString();
        }

        RunHome.text = RunHomeInt.ToString();
        RunAway.text = RunAwayInt.ToString();
        HitHome.text = HitHomeInt.ToString();
        HitAway.text = HitAwayInt.ToString();
        BaseOnBallHome.text = BaseOnBallHomeInt.ToString();
        BaseOnBallAway.text = BaseOnBallAwayInt.ToString(); ;
    }
}
