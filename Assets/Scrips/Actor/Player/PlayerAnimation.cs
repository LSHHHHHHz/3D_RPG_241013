using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    [SerializeField] PlayerController playerController;
    public event Action<bool> onStartPlayerAttackAnim;
    public event Action onEndPlayerAttackAnim;

    public event Action onEndSwapWeapon;
    bool isSwapped = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        MoveAnimation();
        SwapWeapon();
        CheckEndAttackAnim();
        DoPlayerJump();
    }
    void MoveAnimation()
    {
        if (playerController.currentSpeed > 5f)
        {
            anim.SetBool("IsRun", true);
            anim.SetBool("IsWalk", true);
        }
        else if (playerController.currentSpeed > 0.1f)
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
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Player_SwapWeapon"))
        {
            if (stateInfo.normalizedTime < 0.5f)
            {
                isSwapped = false;
            }
            if (stateInfo.normalizedTime >= 0.5f && !isSwapped)
            {
                isSwapped = true;
                onEndSwapWeapon?.Invoke();
                Debug.Log("Swap");
            }
        }
    }
    void CheckEndAttackAnim()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Player_Attack"))
        {
            if (stateInfo.normalizedTime >= 0.5f)
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
    public void DoPlayerJump()
    {
        if (Input.GetButtonDown("Jump") && !playerController.doJump)
        {
            anim.SetTrigger("DoJump");
        }
    }
}
