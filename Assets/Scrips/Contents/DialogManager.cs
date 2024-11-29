using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public int dialogBranch;
    public Transform dialogTransform;
    public DialogSystem dialogSystem;
    public GameObject dialogSysyemObj;
    private bool isTalking = false; 
    private bool dialogEnded = false;
    private bool isStartDialog = false;
    public event Action<int , bool> onEndDialog;
    private void Update()
    {
        if (isTalking && !dialogEnded && isStartDialog)
        {
            if (dialogSystem.UpdateDialog())
            {
                EndDialog();
            }
        }
    }
    public void OpenDialog(int branch, bool startDialog)
    {
        dialogBranch = branch;

        if (dialogSystem == null)
        {
            dialogSystem = Instantiate(dialogSysyemObj, dialogTransform).GetComponent<DialogSystem>();
        }
        else
        {
            dialogSystem.gameObject.SetActive(true);
        }
        dialogSystem.SetBranch(dialogBranch);

        isStartDialog = startDialog;
        isTalking = true;
        dialogEnded = false;
    }

    private void EndDialog()
    {
        isTalking = false; 
        dialogEnded = true;
        isStartDialog = false;
        dialogSystem.gameObject.SetActive(false);
        onEndDialog?.Invoke(dialogBranch,dialogEnded);
    }
}
