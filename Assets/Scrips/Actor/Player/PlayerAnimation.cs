using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PlayerMove playerMove;
    public event Action<bool> onStartPlayerAttackAnim;
    public event Action onEndPlayerAttackAnim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }
    private void Update()
    {
        MoveAnimation();
        SwapWeapon();
        CheckEndAttackAnim();
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
    void CheckEndAttackAnim()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Player_Attack"))
        {
            if (stateInfo.normalizedTime >= 0.5f && !stateInfo.loop)
            {
                onEndPlayerAttackAnim?.Invoke();
                onStartPlayerAttackAnim?.Invoke(false);
            }
            else if (stateInfo.normalizedTime >= 0)
            {
                onStartPlayerAttackAnim?.Invoke(true);
            }
        }
    }
    public void DoLeapJump()
    {
        anim.SetTrigger("DoLeap");
    }
}
