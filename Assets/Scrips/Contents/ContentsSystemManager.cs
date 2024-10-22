using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsSystemManager : MonoBehaviour
{
    public GameObject[] contentsSystemObj;
    [SerializeField] RectTransform transformParent;
    public ContentsSystem[] contentsSystems;
    private void Awake()
    {
        contentsSystems = new ContentsSystem[contentsSystemObj.Length];
        for(int i =0; i < contentsSystemObj.Length; i++)
        {
            contentsSystems[i] = Instantiate(contentsSystemObj[i], transformParent).GetComponent<ContentsSystem>();
            contentsSystems[i].gameObject.SetActive(false);
        }
        EventManager.instance.onTalkNPC += ActiveContentSystem;
    }
    public void ActiveContentSystem(int branch)
    {
        if (contentsSystemObj.Length != 0)
        {
            for (int i = 0; i < contentsSystems.Length; i++)
            {
                if (contentsSystems[i].contentsBranch == branch)
                {
                    contentsSystems[i].gameObject.SetActive(true);
                }
                else
                {
                    contentsSystems[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
