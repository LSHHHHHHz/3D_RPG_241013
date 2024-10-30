using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSkill1 : BaseSkill
{
    [SerializeField] GameObject prefab;
    GameObject prefabInstance;
    public override void ExcuteSkill(Actor actor)
    {
        if (actor is Player player)
        {
            TargettingObject targettingObject = player.targetingObject;
            if (targettingObject == null) return;

            if (targettingObject.targetObj != null)
            {
                if (prefabInstance == null)
                {
                    prefabInstance = Instantiate(prefab, targettingObject.targetObj.transform.position, Quaternion.identity);
                }
                else
                {
                    prefabInstance.SetActive(true);
                }
                foreach (var effectPrefab in prefabInstance.GetComponent<ActSkill1Projectile>().effectTrigger)
                {
                    EffectTrigger effectTrigger = effectPrefab.GetComponent<EffectTrigger>();
                    effectTrigger.SetDamage(10);
                }

                StartCoroutine(DisablePrefabEffect(3f));
            }
            else
            {
                Debug.Log("타겟팅된 오브젝트 없음.");
            }
        }
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
