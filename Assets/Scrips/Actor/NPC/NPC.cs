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
        EventManager.instance.onEndTalkNPC += OpenShopPopupUI;
    }
    private void OnDisable()
    {
        EventManager.instance.onEndTalkNPC -= OpenShopPopupUI;
    }
    private void Update()
    {
        fsmController.FSMUpdate();
        if (npcDetector.isPossibleTalk && Input.GetKeyDown(KeyCode.T))
        {
            EventManager.instance.StartTalkNPC(npcBranch);
        }
    }
    void OpenShopPopupUI(int index)
    {
        if(npcBranch == index)
        {
            ActiveShoPopupUI();
        }
    }
    void ActiveShoPopupUI()
    {
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