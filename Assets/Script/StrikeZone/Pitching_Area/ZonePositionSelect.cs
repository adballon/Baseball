using UnityEngine;

//투수가 스트라이크 존의 어디를 선택했는지 반환

public class ZonePositionSelect : MonoBehaviour
{
    public GameObject btn;
    public void return_position()
    {
        string name = btn.name;
        SelectTarget.Instance.target = PitchingZoneController.Instance.ReturnBtnPos(name);
    }
}
