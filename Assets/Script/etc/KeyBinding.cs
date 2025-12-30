using UnityEngine;

public static class KeyBinding
{
#if UNITY_EDITOR
    // For Unity Editor (keyboard test)
    public static bool IsPitchButtonDown => Input.GetKeyDown(KeyCode.P);
    public static bool IsPitchButtonUp => Input.GetKeyUp(KeyCode.P);
    public static bool IsHitButtonDown => Input.GetKeyDown(KeyCode.H);
    public static bool SpawnBall => Input.GetKeyDown(KeyCode.S);
#else
    // For Meta Quest HMD (OVR)
    public static bool IsPitchButtonDown => OVRInput.GetDown(OVRInput.RawButton.B);
    public static bool IsPitchButtonUp => OVRInput.GetUp(OVRInput.RawButton.B);
    public static bool IsHitButtonDown => OVRInput.GetDown(OVRInput.RawButton.A);
    public static bool SpawnBall => OVRInput.GetDown(OVRInput.RawButton.X);
#endif
}
