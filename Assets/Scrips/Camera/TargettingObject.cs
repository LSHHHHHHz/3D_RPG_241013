using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingObject : MonoBehaviour
{
    public LayerMask layerMask;
    public int distanceObj;
    public GameObject targetObj;
    public GameObject targettingPopupUIprefab;
    TargettingEnemyPopupUI targettingEnemyPopupUI;
    [SerializeField]RectTransform targettingRectTransform;
    [SerializeField] Camera mainCamera;
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectTarget();
        }
    }
    void SelectTarget()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1.0f);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log(hit.collider.gameObject.name);
            BaseEnemy enemy = hit.collider.GetComponent<BaseEnemy>();
            if (enemy != null && Vector3.Distance(transform.position, enemy.transform.position) <= distanceObj)
            {
                targetObj = enemy.gameObject;
                enemy.onDeathEnemy += ClearTarget;
                ShowTargetPopup(enemy.GetEnemyID());
            }
        }
    }
    void ShowTargetPopup(string enemyID)
    {
        if (targettingEnemyPopupUI == null)
        {
            targettingEnemyPopupUI = Instantiate(targettingPopupUIprefab, targettingRectTransform).GetComponent<TargettingEnemyPopupUI>();
            targettingEnemyPopupUI.gameObject.SetActive(true);
            targettingEnemyPopupUI.onDisablePopupUI += ClearTarget;
        }
        else
        {
            targettingEnemyPopupUI.gameObject.SetActive(true);
        }
        targettingEnemyPopupUI.Setdata(enemyID);
    }
    void ClearTarget()
    {
        targetObj = null; 
        targettingEnemyPopupUI.gameObject.SetActive(false);
    }
}

