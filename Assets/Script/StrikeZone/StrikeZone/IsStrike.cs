using UnityEngine;

//공의 스트라이크 존 통과 여부 반환

public class IsStrike : MonoBehaviour
{
    public static IsStrike instance;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) //공이 스트라이크존을 통과
        {
            BallState state = other.GetComponent<BallState>();
            if (state != null)
            {
                state.passedStrikeZone = true; //통과 했다고 알림
            }
        }
    }

    private void Awake()
    {
        instance = this;
    }
}
