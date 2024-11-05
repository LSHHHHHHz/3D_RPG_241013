using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraMotionBlur : BaseMotionBlur
{
    private void OnEnable()
    {
        GameManager.instance.motionBlurManager.onToggleSwordMotionBlur += ToggleCameraMotionBlur;
    }
    private void OnDisable()
    {
        GameManager.instance.motionBlurManager.onToggleSwordMotionBlur -= ToggleCameraMotionBlur;
    }
    void ToggleCameraMotionBlur(bool b)
    {
        if (b)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
