using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

//게임 시작 화면의 도움말 띄우기

public class Helper : MonoBehaviour
{
    GameObject[] titles;
    GameObject panel;

    private void Start()
    {
        titles = UIController.Instance.title;
        panel = UIController.Instance.panel;
    }
    public void help()
    {
        for (int i = 0; i < titles.Length; i++)
        {
            titles[i].SetActive(false);
        }
        panel.SetActive(true);
    }

    public void back()
    {
        for (int i = 0; i < titles.Length; i++)
        {
            titles[i].SetActive(true);
        }
        panel.SetActive(false);
    }
}
