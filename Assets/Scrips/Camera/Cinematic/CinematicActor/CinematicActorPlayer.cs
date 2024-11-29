using System.Collections;
using UnityEngine;

public class CinematicActorPlayer : CinematicActor
{
    PlayerController controller;
    [SerializeField] Vector3 targetPos;
    PlayerAnimation playerAnimation;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponentInChildren<PlayerController>();
        playerAnimation = gameObject.GetComponent<PlayerAnimation>();
    }
    public override void PerformAction()
    {
        StopAllCoroutines();
        StartCoroutine(PlayerPerformAction());
    }
    IEnumerator PlayerPerformAction()
    {
        playerAnimation.enabled = false;
        controller.SetExternalControl(true);
        anim.SetBool("IsWalk", true);
        float elpasedTime = 0;
        while (elpasedTime < 2)
        {
            controller.MoveController(3,(targetPos - controller.transform.position).normalized);
            elpasedTime += Time.deltaTime;
            yield return null;
        }
        controller.MoveController(0, (targetPos - controller.transform.position).normalized);
        controller.SetExternalControl(false);
        playerAnimation.enabled = true;
    }
}
