using TMPro;
using UnityEngine;
using UnityEngine.UI;

//투수의 구종 선택 제어

public class PitchingButtons : MonoBehaviour
{
    public Button btn;
    public void ChangeType()
    {
        string buttonText = btn.GetComponentInChildren<TextMeshProUGUI>().text; //UI의 버튼 정보를 가져와서

        //버튼의 문자열로 구종 선택

        PitchingMyself.Instance.type = buttonText;
    }
}
