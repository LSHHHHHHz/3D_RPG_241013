using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    Rigidbody rigid;
    string randomRewardId;
    [SerializeField] float forcePower = 3f;  
    [SerializeField] float randomRange = 2f;  

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        CreatedDropItem();
    }
    void CreatedDropItem()
    {
        float randomX = Random.Range(-randomRange, randomRange);
        float randomZ = Random.Range(-randomRange, randomRange);

        Vector3 forceDirection = new Vector3(randomX, 1, randomZ).normalized;

        rigid.AddForce(forceDirection * forcePower, ForceMode.Impulse);
    }
    void RandomRewardID()
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue > 0.95f)
        {
            Debug.Log("장비");
        }
        else if (randomValue > 0.8f)
        {
            Debug.Log("포션");
        }
        else if (randomValue > 0.4f)
        {
            Debug.Log("경험치");
        }
        else
        {
            Debug.Log("코인");
        }
    }
}