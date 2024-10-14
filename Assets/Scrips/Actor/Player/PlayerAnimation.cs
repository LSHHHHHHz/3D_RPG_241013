using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PlayerMove playerMove;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        MoveAnimation();
    }
    void MoveAnimation()
    {
        if (playerMove.currentSpeed > 5f)
        {
            anim.SetBool("IsRun", true);
            anim.SetBool("IsWalk", true);
        }
        else if (playerMove.currentSpeed > 0.1f)
        {
            anim.SetBool("IsWalk", true);
            anim.SetBool("IsRun", false);
        }
        else
        {
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsRun", false);
        }
    }
}
