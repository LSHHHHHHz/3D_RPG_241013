using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class NPC : MonoBehaviour
{
    [SerializeField] string npcName;
    [SerializeField] int npcDialogBranch;
    int progressBranch;
    public NPCDetector npcDetector { get; private set; }
    public NPCMove npcMove { get; private set; }
    public Animator anim { get; private set; }
    public FSMController<NPC> fsmController { get; private set; }
    [SerializeField] GameObject shopPopupUIPrefab;
    [SerializeField] Transform shopPopupTransform;
    bool isOpenPopup = false;
    bool isDialogEnd = false;
    ShopPopupUI shopPopupUI;
    public event Action<string> onTalkNPC;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        npcDetector = GetComponent<NPCDetector>();
        npcMove = GetComponent<NPCMove>();
        fsmController = new FSMController<NPC>(this);
    }
    private void OnEnable()
    {
        fsmController.ChangeState(new NPCWalkState());
        GameManager.instance.dialogManager.onEndDialog += CheckEndDialog;
        QuestManager.instance.onClearQuest += NextDialog;
    }
    private void OnDisable()
    {
        GameManager.instance.dialogManager.onEndDialog -= CheckEndDialog;
        QuestManager.instance.onClearQuest -= NextDialog;
    }
    private void Update()
    {
        fsmController.FSMUpdate();

        if (npcDetector.isPossibleTalk && Input.GetKeyDown(KeyCode.T))
        {
            GameManager.instance.dialogManager.OpenDialog(npcDialogBranch, true);
        }
        if (!npcDetector.isPossibleTalk)
        {
            isOpenPopup = false;
        }
        if (progressBranch == npcDialogBranch && isDialogEnd && !isOpenPopup)
        {
            isOpenPopup = true;
            ActiveShoPopupUI();
            QuestManager.instance.ProgressQuest(npcName);
        }
    }

    void CheckEndDialog(int branch, bool boool)
    {
        progressBranch = branch;
        isDialogEnd = boool;
    }
    void NextDialog()
    {
        if (npcName == "MainNPC")
        {
            npcDialogBranch++;
        }
    }
    void ActiveShoPopupUI()
    {
        if (npcName == "MainNPC")
        {
            return;
        }
        if (npcDetector.isPossibleTalk)
        {
            if (shopPopupUI == null)
            {
                shopPopupUI = Instantiate(shopPopupUIPrefab, shopPopupTransform).GetComponent<ShopPopupUI>();
            }
            else
            {
                shopPopupUI.gameObject.SetActive(true);
            }
            shopPopupUI.OpenShopPopup(npcName);
        }
        if (!npcDetector.isPossibleTalk && shopPopupUI != null)
        {
            shopPopupUI.gameObject.SetActive(false);
            isOpenPopup = false;
        }
    }
}