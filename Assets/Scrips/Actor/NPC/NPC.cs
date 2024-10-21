using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class NPC : MonoBehaviour
{
    [SerializeField] string shopNpcName;
    public NPCDetector npcDetector { get; private set; }
    public NPCMove npcMove { get; private set; }
    public Animator anim { get; private set; }
    public FSMController<NPC> fsmController { get; private set; }
    [SerializeField] GameObject shopPopupUIPrefab;
    [SerializeField] Transform shopPopupTransform;
    ShopPopupUI shopPopupUI;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Talk");
        }
        fsmController.FSMUpdate();
        if (npcDetector.isPossibleTalk && Input.GetKeyDown(KeyCode.T))
        {
            if (shopPopupUI == null)
            {
                shopPopupUI = Instantiate(shopPopupUIPrefab, shopPopupTransform).GetComponent<ShopPopupUI>();
            }
            else
            {
                shopPopupUI.gameObject.SetActive(true);
            }
            shopPopupUI.OpenShopPopup(shopNpcName);
        }
        if(!npcDetector.isPossibleTalk && shopPopupUI != null)
        {
            shopPopupUI.gameObject.SetActive(false);
        }
    }
}