using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsSystem : MonoBehaviour
{
    public Player player;
    public int contentsBranch;
    public GameObject dialogSysyemObj;
    public DialogSystem dialogSystem;
    public CameraFollow maincamera;
    public FadeSystem fadeSystem;
    private void OnEnable()
    {
        if (dialogSystem == null)
        {
            dialogSystem = Instantiate(dialogSysyemObj, transform).GetComponent<DialogSystem>();
        }
        else
        {
            dialogSystem.gameObject.SetActive(true);
            StartCoroutine(RunContent());
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
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => dialogSystem.UpdateDialog());
            EventManager.instance.EndTalkNPC(contentsBranch);
        }
        if (maincamera != null)
        {
            yield return StartCoroutine(maincamera.ZoomOut(1));
        }
        if (fadeSystem != null)
        {
            yield return StartCoroutine(fadeSystem.FadeOut());
        }
        gameObject.SetActive(false);
    }
}
