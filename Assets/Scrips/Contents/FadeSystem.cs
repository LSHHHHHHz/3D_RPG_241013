using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSystem : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 1f;

    public IEnumerator FadeIn()
    {
        Debug.Log("���̵��� ����");
        yield return null;
    }

    public IEnumerator FadeOut()
    {
        Debug.Log("���̵�ƿ� ����");
        yield return null;
    }
}