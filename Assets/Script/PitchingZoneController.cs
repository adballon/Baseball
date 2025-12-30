using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PitchingZoneController : MonoBehaviour
{
    public static PitchingZoneController Instance;
    public GameObject[] btn;
    void Awake()
    {
        Instance = this;
    }

    public Vector3 ReturnBtnPos(string name)
    {
        for (int i = 0; i < btn.Length; i++)
        {
            if (btn[i].name == name)
            {
                return btn[i].transform.position;
            }
        }

        return Vector3.zero;
    }
}
