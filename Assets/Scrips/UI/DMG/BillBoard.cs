using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BillBoard : MonoBehaviour
{
    Transform cam;
    private void Start()
    {
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }
        else
        {
            Debug.LogError("카메라 없어요.");
        }
    }
    private void LateUpdate()
    {
        transform.LookAt(cam.position);
    }
}
