using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSkill2 : BaseSkill
{
    [SerializeField] GameObject prefab;
    GameObject prefabInstance;

    public override void ExcuteSkill(Actor actor)
    {
        if (actor is Player player)
        {
            TargettingObject targettingObject = player.targetingObject;
            if (targettingObject == null)
            {
                return;
            }
            if (targettingObject.targetObj != null)
            {
                Vector3 targetPosition = targettingObject.targetObj.transform.position;
                StartCoroutine(ActivateOrCreatePrefab(targetPosition)); 
            }
            else
            {
                Debug.Log("타겟팅된 오브젝트 없음.");
            }
        }
        if (actor is BaseEnemy enemy)
        {
            Player p = GameManager.instance.player;
            if (p == null)
            {
                return;
            }
            if (p != null)
            {
                Vector3 targetPosition = p.transform.position;
                StartCoroutine(ActivateOrCreatePrefab(targetPosition));
            }
            else
            {
                Debug.Log("타겟팅된 오브젝트 없음.");
            }
        }
    }
    private IEnumerator ActivateOrCreatePrefab(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(2f);

        if (prefabInstance == null) 
        {
            prefabInstance = Instantiate(prefab, targetPosition, Quaternion.identity);
        }
        else 
        {
            prefabInstance.transform.position = targetPosition;
            prefabInstance.SetActive(true);
        }
        prefabInstance.GetComponent<ActSkill2Projectile>().SetDamage(20);
        StartCoroutine(DisablePrefabEffect(3f));
    }
    private IEnumerator DisablePrefabEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (prefabInstance != null)
        {
            prefabInstance.SetActive(false);
        }
    }   
}
