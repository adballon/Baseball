using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

//주자 관리

public class BatterRunnerManager : MonoBehaviour
{
    public static BatterRunnerManager Instance;

    [Header("주자 프리팹 및 루 포지션")]
    public GameObject runnerPrefab; // 달리는 주자 프리팹
    public Transform[] basePositions; // 0: 1루, 1: 2루, 2: 3루, 3: 홈
    public List<GameObject> RunnerList; //현재 루상의 주자
    public List<GameObject> updatedRunners; //업데이트 된 주자

    private void Start()
    {
        RunnerList = new List<GameObject>();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnBatterAndRun(int baseHit)
    {
        updatedRunners = new List<GameObject>();

        if (baseHit < 1 || baseHit > 4) return; //1~4루를 움직일 수 있음

        GameObject batter = Instantiate(runnerPrefab); //타자 주자 생성(안타를 침)
        batter.transform.position = basePositions[3].position; //홈에 위치
        RunnerController batterCon = batter.GetComponent<RunnerController>(); //타자 정보를 가져옴

        int batterTargetBase = baseHit - 1; //어디로 움직일 것인가
        batterCon.RunToBase(0, batterTargetBase, basePositions); //이동

        if (baseHit == 4) //홈런
        {
            HittedResult.Instance.HomeRun();
            batterCon.OnReachedFinalBase = () =>
            {
                Destroy(batter); //홈에 도착했으므로 삭제
            };
        }
        else
        {
            batterCon.Loc_Base = baseHit + 1; //현재 위치 저장
            updatedRunners.Add(batter); //업데이트 완료
        }


        foreach (GameObject runner in RunnerList.ToArray()) //현재 루상의 주자
        {
            if (runner == batter) continue; //루상의 주자가 타자면 무시

            RunnerController rcon = runner.GetComponent<RunnerController>();//주자 정보 가져오기
            int fromBase = rcon.Loc_Base; //현재 위치
            int toBase = fromBase + baseHit; //이동 위치

            if (toBase >= 4) // 홈 도착
            {
                rcon.RunToBase(fromBase, 3, basePositions); //홈으로 이동

                HittedResult.Instance.HomeRun(); //점수 + 1

                rcon.OnReachedFinalBase = () =>
                {
                    Destroy(runner); //홈에 도착 했으므로 삭제
                };
            }
            else
            {
                rcon.RunToBase(fromBase, toBase-1, basePositions); //이동
                updatedRunners.Add(runner); // 홈까지 안 간 주자만 유지
            }
        }

        RunnerList = updatedRunners; //주자 리스트 갱신
    }

    public void ClearAllRunners() //루 상의 모든 주자 삭제
    {
        foreach (GameObject runner in RunnerList)
        {
            Destroy(runner); // 오브젝트 삭제
        }

        RunnerList.Clear(); // 리스트 초기화
    }
}
