using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsSystem : MonoBehaviour
{
    public Player player;
    public int contentsBranch;
    public DialogSystem dialogSystem;
    public CameraFollow maincamera;
    public FadeSystem fadeSystem;
    private void OnEnable()
    {
        if (dialogSystem != null)
        {
            dialogSystem.gameObject.SetActive(true);
        }
        StartCoroutine(RunContent());
    }
    private void OnDisable()
    {
        if (dialogSystem != null)
        {
            dialogSystem.gameObject.SetActive(false);
        }
    }
    private IEnumerator RunContent()
    {
        if (maincamera != null)
        {
            yield return StartCoroutine(maincamera.ZoomIn(1));
        }
        if (fadeSystem != null)
        {
            yield return StartCoroutine(fadeSystem.FadeIn());
        }
        if (dialogSystem != null && contentsBranch != -1)
        {
            dialogSystem.SetBranch(contentsBranch);
            yield return new WaitUntil(() => dialogSystem.UpdateDialog());
        }
        if (maincamera != null)
        {
            yield return StartCoroutine(maincamera.ZoomOut(1));
        }
        if (fadeSystem != null)
        {
            yield return StartCoroutine(fadeSystem.FadeOut());
        }
    }
}
