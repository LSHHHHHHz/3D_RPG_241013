using System.Collections;
using UnityEngine;

public class BuffSkill2 : BaseSkill
{
    [SerializeField] GameObject prefab;                  
    [SerializeField] private int maxHpIncreaseAmount = 50; 
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

            player.StartCoroutine(IncreaseMaxHPBuff(player));
        }
    }
    private IEnumerator IncreaseMaxHPBuff(Player player)
    {
        int originalMaxHP = player.status.playerMaxHP;
        int originalCurrentHP = player.status.playerCurrentHP;

        player.status.IncreaseMaxHP(maxHpIncreaseAmount);
        player.status.GetHP(maxHpIncreaseAmount);  

        yield return new WaitForSeconds(skillDuration); 

        player.status.SetMaxHP(originalMaxHP);
        if (player.status.playerCurrentHP > originalCurrentHP)
        {
            player.status.SetCurrentHP(originalCurrentHP);
        }

        prefabInstance.SetActive(false);  
    }
}
