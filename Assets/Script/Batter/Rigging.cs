using Unity.Mathematics;
using UnityEngine;

public class Rigging : MonoBehaviour
{
    public static Rigging Instance;
    public Animator animator;
    public Transform[] targetZoneCenter;
    public Transform target;
    public float up;
    public float elbowUp;
    public float axis;
    public float weight;
    private bool isSwinging = false;
    private bool targetChosen = false;
    public Transform batTransform;

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        animator = GetComponent<Animator>();
        target = targetZoneCenter[7];
    }

    void Update()
    {

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        isSwinging = state.IsName("Hit") && state.normalizedTime < 1f;

        if (isSwinging && !targetChosen)
        {
            Debug.Log($"🎯 타격 대상: {target.name}");
            targetChosen = true;
        }

        if (!isSwinging)
        {
            targetChosen = false;
        }

        string name = target.name;

        if (name == "RB" || name == "CB" || name == "LB")
        {
            weight = 0;
        }
        else if (name == "RC" || name == "CC" || name == "LC")
        {
            weight = 0.025f;
        }
        else if (name == "RT" || name == "CT" || name == "LT")
        {
            weight = 0.07f;
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (animator == null) return;

        Quaternion change = Quaternion.Euler(0, axis, 0);
        batTransform.rotation *= change;

        // 왼손 위치
        Transform leftHand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
        Vector3 leftHandPos = leftHand.position;
        Quaternion leftHandRot = leftHand.rotation;

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos + new Vector3(0f, up, 0f));
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRot);

        // 오른손 위치
        Transform rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);
        Vector3 rightHandPos = rightHand.position;
        Quaternion rightHandRot = rightHand.rotation;

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandPos + new Vector3(0f, up, 0f));
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandRot);

        // 왼쪽 팔꿈치 힌트
        Transform leftUpperArm = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
        Vector3 leftElbowHintPos = leftUpperArm.position + new Vector3(0f, elbowUp, 0f);

        animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, weight);
        animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowHintPos);

        // 오른쪽 팔꿈치 힌트
        Transform rightUpperArm = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
        Vector3 rightElbowHintPos = rightUpperArm.position + new Vector3(0f, elbowUp, 0f);

        animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, weight);
        animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowHintPos);
    }
}
