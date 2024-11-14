using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class InterationUI : MonoBehaviour
{
    DetectorBase detectBase;
    Player player;
    Image panelImage;
    private void Awake()
    {
        detectBase = GetComponentInParent<DetectorBase>();
        player = FindAnyObjectByType<Player>();
        panelImage = GetComponent<Image>();
    }
    private void Update()
    {
        UpdateImageAlpha();
    }
    void UpdateImageAlpha()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 objectPosition = new Vector3(transform.position.x, 0, transform.position.z);

        float dis = Vector3.Distance(objectPosition, playerPosition);
        float detectRange = detectBase.GetDetectRange();
        float activeRange = detectBase.GetActiveDetectRange();

        if (dis <= detectRange )
        {
            float t = (detectRange - dis) / (detectRange - activeRange);
            SetAlpha(t);
        }
        else
        {
            SetAlpha(0f); 
        }
    }
    void SetAlpha(float alphaValue)
    {
        Color color = panelImage.color;
        color.a = alphaValue;
        panelImage.color = color;
    }
}