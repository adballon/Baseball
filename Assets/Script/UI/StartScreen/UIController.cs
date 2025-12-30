using UnityEngine;

//게임 시작 화면 UI 제어

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public GameObject[] title;
    public GameObject panel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for(int i=0;i<title.Length;i++)
        {
            title[i].SetActive(true);
        }
        panel.SetActive(false);
    }
}
