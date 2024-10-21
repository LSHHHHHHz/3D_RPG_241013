using UnityEngine;

public class WarePoint : MonoBehaviour
{
    public Transform[] warePointPos;

    private void Awake()
    {
        warePointPos = GetComponentsInChildren<Transform>();
    }
     
    private void OnDrawGizmos()
    {
        if (warePointPos != null && warePointPos.Length >= 2)
        {
            Gizmos.color = Color.yellow;

            for (int i = 1; i < warePointPos.Length; i++)
            {
                if (i == warePointPos.Length - 1)
                {
                    Gizmos.DrawLine(warePointPos[i].position, warePointPos[1].position);  
                }
                else
                {
                    Gizmos.DrawLine(warePointPos[i].position, warePointPos[i + 1].position);  
                }
            }
        }
    }
}
