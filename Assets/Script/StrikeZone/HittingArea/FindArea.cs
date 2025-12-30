using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//공이 스트라이크 존 어디를 통과했는가

public class FindArea : MonoBehaviour
{
    public static FindArea Instance;
    public GameObject[] cubes; //스트라이크 존을 구역별로 나눔
    public HashSet<Collider> detectedBalls = new HashSet<Collider>(); //공의 이중 감지를 막기위한 집합
    public SweetSpot camerabat;
    public SweetSpot battertbat;

    void Awake()
    {
        Instance = this;
    }
    void FixedUpdate()
    {
        Collider targetBall = null;
        GameObject closestCube = null;
        float minDist = float.MaxValue;

        //모든 큐브에서 Ball이 가장 가까운 곳 찾기
        foreach (var cube in cubes) //큐브별로 검사
        {
            if (cube == null) continue;

            Vector3 center = cube.transform.position; //큐브의 현재 위치
            Vector3 halfSize = cube.transform.localScale / 2f; //큐브의 크기를 반으로 자름
            Quaternion rotation = cube.transform.rotation; //큐브의 회전 정보

            Collider[] hits = Physics.OverlapBox(center, halfSize, rotation); //큐브를 검사해서
            foreach (var hit in hits) //공이 통과를 했는가?
            {
                if (hit.CompareTag("Ball") && !detectedBalls.Contains(hit)) //통과를 했다면
                {
                    float dist = Vector3.Distance(hit.transform.position, center); //큐브의 중심과의 거리를 구해
                    if (dist < minDist) //사전에 지정된 거리보다 짧다면
                    {
                        minDist = dist;
                        closestCube = cube;
                        targetBall = hit;
                        //그 큐브로 업데이트
                    }
                }
                // if (hit.CompareTag("Spot"))
                // {
                //     Debug.Log(cube);
                // }

                //공이 들어온 큐브에 배트도 같이 들어왔는가
                if (targetBall != null && closestCube != null) //공이 큐브에 들어오고 하나만 선택됨
                {
                    Vector3 centerb = closestCube.transform.position; //그 큐브의 위치
                    Vector3 halfSizeb = closestCube.transform.localScale / 2f; //큐브의 크기
                    Quaternion rotationb = closestCube.transform.rotation; //큐브의 회전 정보

                    Collider[] finalHits = Physics.OverlapBox(centerb, halfSizeb, rotationb); //큐브 검사

                    foreach (var hitb in finalHits) //각 큐브별로 검사
                    {
                        BallState state = targetBall.GetComponent<BallState>();
                        var bat = hitb.GetComponentInParent<BatController>();
                        if (state != null && hitb.CompareTag("Spot")) //배트가 들어왔다
                        {
                            state.hit.batcontrol = bat.gameObject; //배트의 정보를 가져오고
                            state.isHit = true; //맞았다고 알리고
                            camerabat.settingTrigger(true);
                            battertbat.settingTrigger(true);
                            StartCoroutine(sweetSpotChange());
                            detectedBalls.Add(targetBall); //공이 이중으로 감지되는 것을 막음
                            break;
                        }
                    }
                }
            }
        }
    }

    public IEnumerator sweetSpotChange()
    {
        yield return new WaitForSeconds(2f);
        camerabat.settingTrigger(false);
        battertbat.settingTrigger(false);
    }
}