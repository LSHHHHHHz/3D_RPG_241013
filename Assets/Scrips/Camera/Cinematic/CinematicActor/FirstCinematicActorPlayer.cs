using System.Collections;
using UnityEngine;

public class FirstCinematicActorPlayer : CinematicActor
{
    PlayerController controller;
    [SerializeField] Vector3 targetPos;
    private void Awake()
    {
        controller = GetComponentInChildren<PlayerController>();
    }
    public override void PerformAction()
    {
        StopAllCoroutines();
        StartCoroutine(PlayerPerformAction());
    }
    IEnumerator PlayerPerformAction()
    {
        controller.SetExternalControl(true);
        float elpasedTime = 0;
        while (elpasedTime < 2)
        {
            controller.MoveController(3,(targetPos - controller.transform.position).normalized);
            elpasedTime += Time.deltaTime;
            yield return null;
        }
        controller.MoveController(0, (targetPos - controller.transform.position).normalized);
        controller.SetExternalControl(false);
    }
}
