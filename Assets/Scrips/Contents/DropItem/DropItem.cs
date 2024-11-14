using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DropItem : MonoBehaviour
{
    Rigidbody rigid;
    protected Player player;
    string enemyID;
    [SerializeField] Transform dropEffect;
    [SerializeField] float forcePower = 3f;
    [SerializeField] float randomRange = 0.3f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        player = FindObjectOfType<Player>();
    }
    private void OnEnable()
    {
        dropEffect.gameObject.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(ActiveEffect());
    }
    private void OnDisable()
    {
        dropEffect.gameObject.SetActive(false);
        rigid.isKinematic = false;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 1f)
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
    IEnumerator ActiveEffect()
    {
        float elapsedTime = 0;
        while(elapsedTime <2)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        dropEffect.gameObject.SetActive(true);
        rigid.isKinematic = true;
    }
    public abstract void AcquiredByPlayer();
    public abstract void SetData(string id);
}