using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingObject : MonoBehaviour
{
    public LayerMask layerMask;
    public int distanceObj;
    public GameObject targetObj;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectTarget();
        }
    }
    void SelectTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask))
        {
            BaseEnemy enemy = hit.collider.GetComponent<BaseEnemy>();
            if (enemy != null && Vector3.Distance(transform.position, enemy.transform.position) <= distanceObj)
            {
                targetObj = enemy.gameObject;
                Debug.Log(targetObj.name);
            }
        }
    }

}

