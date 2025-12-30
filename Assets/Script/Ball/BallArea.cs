using UnityEngine;

//볼의 상.하.좌.우 범위를 계산
public class BallArea : MonoBehaviour
{
    public float radius = 0.002f;
    public Transform ball;
    public Vector3 center;
    public float left;
    public float right;
    public float up;
    public float down;
    public float back;

    void Update()
    {
        center = ball.position;
        left = (center - ball.right * radius).x;
        right = (center + ball.right * radius).x;
        up = (center + ball.up * radius).y;
        down = (center - ball.up * radius).y;
        back = (center - ball.forward * radius).z;
    }
}
