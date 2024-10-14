using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PlayerMove playerMove;

    public string newDefaultAnimation; 
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        MoveAnimation();
        SwapWeapon();
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
    void SwapWeapon()
    {
        if (Input.GetButtonDown("Swap"))
        {
            anim.SetTrigger("DoSwap");
        }
    }
}
