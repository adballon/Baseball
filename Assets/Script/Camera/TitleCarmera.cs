using Unity.Mathematics;
using UnityEngine;

public class TitleCarmera : MonoBehaviour
{
    Vector3 startloc = new Vector3(112.8f, 1.5f, 112.8f);
    Quaternion startrot = Quaternion.Euler(0, 225, 0);
    void Start()
    {
        transform.position = startloc;
        transform.rotation = startrot;
    }
}
