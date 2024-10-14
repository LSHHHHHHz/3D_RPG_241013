using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSystem : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 1f;

    public IEnumerator FadeIn()
    {
        Debug.Log("페이드인 시작");
        yield return null;
    }

    public IEnumerator FadeOut()
    {
        Debug.Log("페이드아웃 시작");
        yield return null;
    }
}