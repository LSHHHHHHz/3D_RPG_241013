using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : MonoBehaviour
{
    public void ShowHitEffect(Vector3 thisPos, Vector3 attackerPos)
    {
        transform.position = thisPos + new Vector3(0,1.3f,0);

        Vector3 direction = (thisPos - attackerPos).normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        gameObject.SetActive(true); 
        StartCoroutine(ActiveText());
    }
    IEnumerator ActiveText()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}