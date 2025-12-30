using UnityEngine;

//심판 정보 관리

public class Refree_test : MonoBehaviour
{
    public static Refree_test Instance;
    Rigidbody[] rb;
    Animator animator;

    void Start()
    {
        rb = GetComponentsInChildren<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        Kinetic(true);
    }

    void Update()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        if (info.IsName("Judge") && info.normalizedTime >= 1.0f) 
        {
            judge(false);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void judge(bool b)
    {
        animator.SetBool("Judge", b);
    }

    void Kinetic(bool b)
    {
        foreach (var rigidchildren in rb)
        {
            rigidchildren.isKinematic = b;  // 초기에는 꺼놓음
        }
    }
}
