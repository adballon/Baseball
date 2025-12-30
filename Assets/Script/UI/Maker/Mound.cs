using UnityEngine;

//타자 마운드 만들기
public class Mound : MonoBehaviour
{
    void Start()
    {
        GameObject mound = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        mound.transform.localScale = new Vector3(5.48f, 0.125f, 5.48f);
        mound.transform.position = new Vector3(113f, 0f, 113f);
        mound.GetComponent<Renderer>().material.color = new Color(0.6f, 0.4f, 0.2f); // 흙색

        // 피칭 플레이트
        GameObject plate = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plate.transform.localScale = new Vector3(0.6f, 0.1f, 0.15f);
        plate.transform.position = new Vector3(113f, 0.3f, 113f);
        plate.GetComponent<Renderer>().material.color = Color.white;
    }
}
