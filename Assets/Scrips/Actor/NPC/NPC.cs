using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class NPC : MonoBehaviour
{
    [SerializeField] string npcName;
    [SerializeField] int npcBranch;
    public NPCDetector npcDetector { get; private set; }
    public NPCMove npcMove { get; private set; }
    public Animator anim { get; private set; }
    public FSMController<NPC> fsmController { get; private set; }
    [SerializeField] GameObject shopPopupUIPrefab;
    [SerializeField] Transform shopPopupTransform;
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
    }
    private void OnDisable()
    {
    }
    private void Update()
    {
        fsmController.FSMUpdate();
        if (npcDetector.isPossibleTalk && Input.GetKeyDown(KeyCode.T))
        {
            ActiveShoPopupUI();
            QuestManager.instance.ProgressQuest(GoalType.TalkNPC, npcName);
        }
    }
    void OpenShopPopupUI(int index)
    {
        if (npcBranch == index)
        {
            ActiveShoPopupUI();
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
        }
    }
}