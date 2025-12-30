using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// 게임의 진행 상태를 관리

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    string[] inning = { "FirstHome", "FirstAway", "SecondHome", "SecondAway", "ThirdHome", "ThirdAway" }; // 이닝 이름
    public int statusint = 0; // 이닝 상태를 정수로 반환
    public string status; // 이닝 상태를 문자열로 반환
    public bool InningChange = false; // 이닝 교체 여부
    public BallCountAway Outside; // 아웃 카운트
    public bool isAttack = false; // 공격 팀 여부

    public GameObject[] BatterNeedeed; // 타자 관련 오브젝트
    public GameObject[] PitcherNeeded; // 캔버스, 피칭존, 타자 오브젝트 등

    void Start()
    {
        Update_Status();
    }

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        Update_Status();
    }

    void Update_Status() // 이닝 상태 업데이트
    {
        if (statusint == inning.Length) // 모든 이닝이 끝나면
        {
            Debug.Log("Game End"); // 게임 종료

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        if (Outside.isThreeOut && Outside != null) // 아웃카운트가 3이라면
        {
            statusint++; // 이닝 전환
            Outside.isThreeOut = false; // 아웃 카운트 리셋
            BatterRunnerManager.Instance.ClearAllRunners(); // 주자 전부 제거
            VR_Camera.Instance.CycleParent(); // VR 카메라 부모 변경 (시점 전환 등)
        }

        status = inning[statusint]; // 현재 이닝 상태 업데이트
    }
}
