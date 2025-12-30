using UnityEngine;

//배트의 정보

public class BatController : MonoBehaviour
{
    private Vector3 lastPosition;
    public Vector3 Velocity { get; private set; }

    public float batspeed = 1.0f;

    void Start()
    {
        lastPosition = transform.position; //배트의 현재 위치를 이전 정보로 저장
    }

    void Update()
    {
        Velocity = (transform.position - lastPosition) / Time.deltaTime * batspeed; //배트의 힘을 전 위치에 대해 얼마나 움직였냐를 봄, 추가적으로 상수를 곱함.
        lastPosition = transform.position;
    }

    //한 프레임(Update)에 얼마나 많이 움직였냐를 보여줌
    //많이 움직일 수록 이전 좌표값에 비해 현재 좌표가 많이 움직였다는 것으로 판단.
}