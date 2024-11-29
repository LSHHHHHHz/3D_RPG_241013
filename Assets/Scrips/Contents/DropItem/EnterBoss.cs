using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnterBoss : MonoBehaviour
{
    Player player;
    [SerializeField] int bossNum;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position,player.transform.position)< 3 && Input.GetMouseButtonDown(1))
        {
            EnterBossStage(bossNum);
        }
    }
    void EnterBossStage(int num)
    {
        GameManager.instance.chematicManager.ActionCinematic(num);
    }
}