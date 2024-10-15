using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class NPC : MonoBehaviour
{
    public NPCDetector npcDetector {  get; private set; }
    public NPCMove npcMove{  get; private set; }
    public Animator anim { get; private set; }
    public FSMController<NPC> fsmController {  get; private set; } 
    private void Awake()
    {
        anim = GetComponent<Animator>();
        npcDetector = GetComponent<NPCDetector>();
        npcMove = GetComponent<NPCMove>();
        fsmController = new FSMController<NPC> (this);
    }
    private void OnEnable()
    {
        fsmController.ChangeState(new NPCWalkState());
    }
    private void Update()
    {
        fsmController.FSMUpdate();
    }
}