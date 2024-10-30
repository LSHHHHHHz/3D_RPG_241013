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
    public event Action onDodge;
    bool isSwapped = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        EventManager.instance.onActiveSkill += SkillAnim;
    }
    private void OnDisable()
    {
        EventManager.instance.onActiveSkill -= SkillAnim;
    }
    private void Update()
    {
        MoveAnimation();
        SwapWeapon();
        CheckEndAttackAnim();
        DoPlayerJump();
    }
    void SkillAnim(string name)
    {
        StopAllCoroutines();
        GameDBEntity db = GameManager.instance.gameDB.GetProfileDB(name);
        if(db.dataType == "active")
        {
            if (db.dataID == "activeSkill1")
            {
                anim.SetTrigger("DoActiveSkill1");
                StartCoroutine(DisablePlayerController(0.4f));
            }
            if (db.dataID == "activeSkill2")
            {
                anim.SetTrigger("DoActiveSkill2");
                StartCoroutine(DisablePlayerController(2.3f));
            }
        }
        if(db.dataType =="passive")
        {
            anim.SetTrigger("DoBuffSkill");
            StartCoroutine(DisablePlayerController(2.3f));
        }
    }
    private IEnumerator DisablePlayerController(float duration)
    {
        playerController.enabled = false; 
        yield return new WaitForSeconds(duration); 
        playerController.enabled = true; 
    }
    void MoveAnimation()
    {
        if (playerController.currentSpeed > 5.1f)
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
