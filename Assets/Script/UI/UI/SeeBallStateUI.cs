using UnityEngine;
using System.Collections;

public class SeeBallStateUI : MonoBehaviour
{
    public static SeeBallStateUI Instance;

    public GameObject NoticeStrike;
    public GameObject NoticeBall;
    public GameObject NoticeOut;
    public GameObject NoticeOne;
    public GameObject NoticeTwo;
    public GameObject NoticeThree;
    public GameObject NoticeHomerun;
    public GameObject NoticeFoul;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        NoticeStrike.SetActive(false);
        NoticeBall.SetActive(false);
        NoticeOut.SetActive(false);
        NoticeOne.SetActive(false);
        NoticeTwo.SetActive(false);
        NoticeThree.SetActive(false);
        NoticeHomerun.SetActive(false);
        NoticeFoul.SetActive(false);
    }

    public IEnumerator SeeGameObject(GameObject obj, float delay)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}
