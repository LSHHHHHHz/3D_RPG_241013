using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuffSkill1 : BaseSkill
{
    [SerializeField] GameObject prefab;  
    [SerializeField] private int attackIncreaseAmount = 10;
    [SerializeField] private float skillDuration = 5f;

    private GameObject prefabInstance;   

    public override void ExcuteSkill(Actor actor)
    {
        if (actor is Player player)
        {
            if (prefabInstance == null)
            {
                prefabInstance = Instantiate(prefab, player.transform.position, Quaternion.identity);
                prefabInstance.transform.SetParent(player.transform); 
            }
            else
            {
                prefabInstance.SetActive(true);  
            }
            player.StartCoroutine(IncreaseAttackBuff(player));
        }
    }

    private IEnumerator IncreaseAttackBuff(Player player)
    {
        player.stats.IncreaseAttack(attackIncreaseAmount);  

        yield return new WaitForSeconds(skillDuration);     

        player.stats.DecreaseAttack(attackIncreaseAmount);  
        prefabInstance.SetActive(false);                
    }
}
