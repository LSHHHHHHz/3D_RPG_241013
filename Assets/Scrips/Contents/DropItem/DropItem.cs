using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DropItem : MonoBehaviour
{
    Rigidbody rigid;
    protected Player player;
    string enemyID;    
    [SerializeField] float forcePower = 3f;
    [SerializeField] float randomRange = 0.3f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            AcquiredByPlayer();
            gameObject.SetActive(false);
        }
    }
    public void CreatedDropItem(Vector3 enemyPos)
    {
        transform.position = enemyPos;
        float randomX = Random.Range(-randomRange, randomRange);
        float randomZ = Random.Range(-randomRange, randomRange);

        Vector3 forceDirection = new Vector3(randomX, 1, randomZ).normalized;

        rigid.AddForce(forceDirection * forcePower, ForceMode.Impulse);
    }
    public abstract void AcquiredByPlayer();
    public abstract void SetData(string id);
}