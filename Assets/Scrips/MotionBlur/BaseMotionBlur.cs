using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BaseMotionBlur : MonoBehaviour
{
    MotionBlurManager motionBlurManager;
    private void Awake()
    {
        motionBlurManager = GameManager.instance.motionBlurManager;
    }
}
