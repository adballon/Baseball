using UnityEngine;

//포수 정보 및 애니메이션 관리
public class Catching_Test : MonoBehaviour
{
    public static Catching_Test Instance;
    Animator animator;
    Rigidbody[] rb;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        rb = GetComponentsInChildren<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        Kinetic(true);
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        if (info.IsName("Throw") && info.normalizedTime >= 1.0f)
        {
            Throw_Ball(false);
        }
    }

    public void Throw_Ball(bool b)
    {
        animator.SetBool("Throw", b);
    }

    void Kinetic(bool b)
    {
        foreach (var rigidchildren in rb)
        {
            rigidchildren.isKinematic = b;
        }
    }
}
