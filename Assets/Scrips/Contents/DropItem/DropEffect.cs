using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropEffect : MonoBehaviour
{
    private void OnDisable()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }
}