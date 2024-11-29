using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] string amountTextPrefabPath;
    [SerializeField] string getHitPrefabPath;
    public void GenerateText(string amount, Vector3 pos, string c)
    {
        AmountText text = GameManager.instance.poolManager.GetObjectFromPool(amountTextPrefabPath).GetComponent<AmountText>();
        text.ShowDamageText(amount, pos + new Vector3(0, 2, 0), c);
    }
    public void GenerateGetHitPrefab(Vector3 pos, Vector3 targetPos)
    {
        GetHit getHit = GameManager.instance.poolManager.GetObjectFromPool(getHitPrefabPath).GetComponent<GetHit>();
        getHit.ShowHitEffect(pos, targetPos);
    }
}
