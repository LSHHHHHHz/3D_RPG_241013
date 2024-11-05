using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class PlayerAction : MonoBehaviour
{
    Animator anim;
    bool isPossibleMeleeAttack = true;
    public static event Action<bool> onMeleeAttack;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsPointerInUI())
        {
            isPossibleMeleeAttack = false;
        }
        else
        {
            isPossibleMeleeAttack = true;
        }

        if (isPossibleMeleeAttack)
        {
            ActionMeleeAttack();
        }
    }

    private void OnEnable()
    {
        EventManager.instance.onOpenPopup += PossibleAttack;
        onMeleeAttack += GameManager.instance.motionBlurManager.ActivateSwordMotionBlur;
    }

    private void OnDisable()
    {
        EventManager.instance.onOpenPopup -= PossibleAttack;
        onMeleeAttack -= GameManager.instance.motionBlurManager.ActivateSwordMotionBlur;
    }
    void PossibleAttack(bool possible)
    {
        isPossibleMeleeAttack = possible;
        anim.SetBool("IsAttack", false);
    }
    void ActionMeleeAttack()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = stateInfo.normalizedTime % 1;

        if (Input.GetMouseButtonDown(0))
        {
            if (isPossibleMeleeAttack)
            {
                anim.SetBool("IsAttack", true);
                onMeleeAttack?.Invoke(true);
            }
            if (normalizedTime >= 0.5f)
            {
                if (stateInfo.IsName("Player_Attack1"))
                {
                    anim.SetBool("IsAttack2", true);
                    onMeleeAttack?.Invoke(true);
                }
                else if (stateInfo.IsName("Player_Attack2"))
                {
                    anim.SetBool("IsAttack3", true);
                    onMeleeAttack?.Invoke(true);
                }
            }
        }
        if ((stateInfo.IsName("Player_Attack1") || stateInfo.IsName("Player_Attack2") || stateInfo.IsName("Player_Attack3")) && normalizedTime > 0.96f)
        {
            anim.SetBool("IsAttack2", false);
            anim.SetBool("IsAttack3", false);
            onMeleeAttack?.Invoke(false);
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("IsAttack", false);
        }
    }

    private bool IsPointerInUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
