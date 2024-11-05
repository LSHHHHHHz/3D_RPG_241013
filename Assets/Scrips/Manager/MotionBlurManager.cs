using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MotionBlurManager : MonoBehaviour
{
    public Action<bool> onToggleCameraMotionBlur;
    public Action<bool> onToggleSwordMotionBlur;
    public void ActivateCameraMotionBlur(bool b)
    {
        onToggleCameraMotionBlur?.Invoke(b);
    }
    public void ActivateSwordMotionBlur(bool b)
    {
        onToggleSwordMotionBlur?.Invoke(b);
    }
}
