using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAction : MonoBehaviour
{
    Animator anim;
    bool isPossibleMeleeAttack = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isPossibleMeleeAttack)
        {
            ActionMeleeAttack();
        }
    }
    private void OnEnable()
    {
        EventManager.instance.onOpenPopup += PossibleAttack;
    }
    private void OnDisable()
    {
        EventManager.instance.onOpenPopup -= PossibleAttack;
    }
    void PossibleAttack(bool possible)
    {
        isPossibleMeleeAttack = possible;
        anim.SetBool("IsAttack", false);
    }
    void ActionMeleeAttack()
    {
        if (Input.GetMouseButtonDown(0) && isPossibleMeleeAttack)
        {
            anim.SetBool("IsAttack", true);
        }
        if(Input.GetMouseButtonUp(0))
        {
            anim.SetBool("IsAttack", false);
        }
    }
}
