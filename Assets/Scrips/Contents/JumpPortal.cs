using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPortal : MonoBehaviour
{
    Player player;
    public GameObject[] jumpPortalPos;
    private Vector3 targetPos;
    private float portalRange = 1;

    private float updateInterval = 0.3f;
    private float elpasedTime = 0f;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }
    void Update()
    {
        if(elpasedTime < updateInterval)
        {
            elpasedTime += Time.deltaTime;
        }
        else
        {
            elpasedTime = 0;
            FindPlayer();
        }
        foreach (GameObject go in jumpPortalPos)
        {
            if(Vector3.Distance(go.gameObject.transform.position,player.transform.position)<1 && targetPos != Vector3.zero && Input.GetMouseButtonDown(1))
            {
                EventManager.instance.LeapPortalPlayer(targetPos);
                targetPos = Vector3.zero;
            }
        }
    }
    void FindPlayer()
    {
        for (int i = 0; i < jumpPortalPos.Length; i++)
        {
            if (Vector3.Distance(jumpPortalPos[i].transform.position, player.transform.position) < portalRange)
            {
                if (i == 0)
                {
                    targetPos = jumpPortalPos[1].transform.position;
                }
                else
                {
                    targetPos = jumpPortalPos[0].transform.position;
                }               
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < jumpPortalPos.Length; i++)
        {
            Gizmos.DrawWireSphere(jumpPortalPos[i].transform.position, portalRange);
        }
    }
}