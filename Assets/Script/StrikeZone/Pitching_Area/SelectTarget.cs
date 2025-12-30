using UnityEngine;

//타자가 스트라이크 존의 어디를 선택했는지를 반환

public class SelectTarget : MonoBehaviour
{
    public static SelectTarget Instance;

    public Vector3 target = Vector3.zero;

    private void Awake()
    {
        Instance = this;
    }
}
