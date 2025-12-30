using UnityEngine;

public class SweetSpot : MonoBehaviour
{
    public CapsuleCollider Collider;

    void Start()
    {
        Collider = GetComponent<CapsuleCollider>();
    }
    public void settingTrigger(bool b)
    {
        Collider.isTrigger = b;
    }
}
