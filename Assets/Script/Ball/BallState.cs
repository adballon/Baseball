using TMPro;
using Unity.VisualScripting;
using UnityEngine;

//공의 상태 반환
//공이 배트에 맞았는지 여부, 스트라이크 통과 여부

public class BallState : MonoBehaviour
{
    public bool passedStrikeZone = false; //
    public bool isHit = false;


    public BallHit hit;

    void Start()
    {
        hit = GetComponent<BallHit>();
    }

    void Update()
    {
        if(isHit) //공이 배트에 맞았냐
        {
            hit.hit(); //맞은 뒤 행동 실행
            isHit = false; //안 맞은 상태로 반환
        }
    }
}
