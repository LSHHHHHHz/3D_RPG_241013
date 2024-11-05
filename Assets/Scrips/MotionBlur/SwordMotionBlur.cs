using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SwordMotionBlur : BaseMotionBlur
{
    PostProcessVolume volume;
    private void Awake()
    {
        volume = GetComponent<PostProcessVolume>();
    }
    private void OnEnable()
    {
        GameManager.instance.motionBlurManager.onToggleSwordMotionBlur += ToggleSwordMotionBlur;
    }
    private void OnDisable()
    {
        GameManager.instance.motionBlurManager.onToggleSwordMotionBlur -= ToggleSwordMotionBlur;
    }
    void ToggleSwordMotionBlur(bool b)
    {
        if(b)
        {
            volume.enabled = true;
        }
        else
        {
            volume.enabled = false;
        }
    }
}
