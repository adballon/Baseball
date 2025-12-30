using UnityEngine;

//지정된 구종 관리

public class ThrowBall : MonoBehaviour
{
    public static ThrowBall Instance;

    public string[] pitchtype = {"FourSeam", "ChangeUp", "CurveBall"};
    void Awake()
    {
        Instance = this;
    }

    public void throwBall(string name)
    {
        switch(name)
        {
            case "FourSeam":
                PitchType.Instance.FourSeam(); //포심 던지기
                break;
            case "CurveBall":
                PitchType.Instance.CurveBall(); //커브 던지기
                break;
            case "ChangeUp":                   
                PitchType.Instance.ChangeUp(); //체인지업 던지기
                break;
            default:
                PitchType.Instance.FourSeam(); //선택이 안 된 경우 포심 던지기
                break;
        }
    }
}
