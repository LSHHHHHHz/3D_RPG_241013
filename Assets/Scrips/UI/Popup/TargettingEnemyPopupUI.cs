using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
public class TargettingEnemyPopupUI : MonoBehaviour
{
    [SerializeField] Image enemyImage;
    [SerializeField] Text enemyName;
    [SerializeField] Text enemyRewardDescription;

    public event Action onDisablePopupUI;
    public void Setdata(string enemyID)
    {
        StopAllCoroutines();
        MonsterEntity enemyDB = GameManager.instance.gameDB.GetEnemyProfileDB(enemyID);
        enemyImage.sprite = Resources.Load<Sprite>(enemyDB.monsterIconPath);
        enemyName.text = enemyDB.monsterName;
        enemyRewardDescription.text = enemyDB.monsterRewardDescription;
        StartCoroutine(OnDisableEnenyPopupUI());
    }
    IEnumerator OnDisableEnenyPopupUI()
    {
        float elapsedTime = 0;
        while(elapsedTime <3)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        onDisablePopupUI?.Invoke();
    }
}
