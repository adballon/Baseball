using TMPro;
using Unity.VisualScripting;
using UnityEngine;

//카메라의 위치 등 제어

public class VR_Camera : MonoBehaviour
{
    public static VR_Camera Instance;

    private Transform xrOriginTransform; //현재 위치
    private Transform currentParent; //현재 위치한 오브젝트

    [Header("고정 위치/회전")]
    public Vector3 fixedPosition = Vector3.zero; //카메라 위치
    public Quaternion fixedRotation = Quaternion.identity; //카메라 회전

    [Header("부모 리스트")]
    public Transform[] predefinedParents;    // 부모 후보 배열

    private int currentParentIndex = 0; //카메라의 이동 오브젝트

    [Header("카메라가 위치할 오브젝트")]
    public GameObject Pitcher;
    public GameObject Batter;

    void Awake()
    {
        Instance = this;

        xrOriginTransform = transform;
        
        fixedRotation = Quaternion.Euler(0, 180, 0);

        Pitcher.SetActive(true);
        Batter.SetActive(false);

        ChangeParent(Pitcher.transform);
        SetActive("Pitcher");
        //처음 실행시에 투수에게 붙임
    }

    void LateUpdate()
    {
        // 매 프레임 XR Origin을 고정 위치/회전으로 덮어쓰기
        xrOriginTransform.localPosition = fixedPosition;
        xrOriginTransform.localRotation = fixedRotation;

        // if (KeyBinding.ChangeParent) //신호를 주면 오브젝트를 바꿈
        // {
        //     CycleParent();
        // }
    }

    public void CycleParent() //오브젝트를 바꿈
    {
        if (predefinedParents.Length == 0) return;

        if (Pitcher.activeSelf)
        {
            Batter.SetActive(true);
            ChangeParent(Batter.transform); //위치를 바꿈
            Pitcher.SetActive(false);

            SetActive("Batter"); //타자 위치에 올 때 필요한 오브젝트 활성화
            GameState.Instance.isAttack = true; //투수 공 던지기 자동화
            fixedRotation = Quaternion.Euler(0, 0, 0);

        }
        else if (Batter.activeSelf)
        {
            Pitcher.SetActive(true);
            ChangeParent(Pitcher.transform); //위치를 바꿈
            Batter.SetActive(false);

            SetActive("Pitcher"); //투수 위치에 올 때 필요한 오브젝트 활성화
            GameState.Instance.isAttack = false; //투수가 공을 던짐
            fixedRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void ChangeParent(Transform newParent) //오브젝트 바꿈
    {
        if (newParent == null) return;

        // 현재 월드 좌표 저장
        Vector3 worldPos = xrOriginTransform.position;
        Quaternion worldRot = xrOriginTransform.rotation;

        // 부모 변경
        xrOriginTransform.SetParent(newParent);

        // 월드 좌표 유지
        xrOriginTransform.position = worldPos;
        xrOriginTransform.rotation = worldRot;

        currentParent = newParent;
    }

    public void SetActive(string jobs) //투수, 타자에게 필요한 오브젝트 활성화
    {
        GameObject[] pit = GameState.Instance.PitcherNeeded;
        GameObject[] bat = GameState.Instance.BatterNeedeed;
        if (jobs == "Pitcher")
        {
            for (int i = 0; i < pit.Length; i++)
            {
                pit[i].SetActive(true);
            }

            for (int i = 0; i < bat.Length; i++)
            {
                bat[i].SetActive(false);
            }
        }
        else if (jobs == "Batter")
        {
            for (int i = 0; i < pit.Length; i++)
            {
                pit[i].SetActive(false);
            }

            for (int i = 0; i < bat.Length; i++)
            {
                bat[i].SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Not Found!");
        }
    }
}
