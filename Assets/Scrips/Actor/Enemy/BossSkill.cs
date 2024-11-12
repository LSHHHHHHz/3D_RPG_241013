using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class BossSkill : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    GameObject prefabInstance;
    public void ExcuteSkill(Actor target)
    {
        if (target == null)
        {
            return;
        }
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position;
            StartCoroutine(ActivateOrCreatePrefab(targetPosition));
        }
        else
        {
            Debug.Log("타겟팅된 오브젝트 없음.");
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
        prefabInstance.GetComponent<ActSkill2Projectile>().SetSkill(new EnemySkillStrategy(), 33);
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
