using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager poolManager;
    public DropItemManager dropItemManager;
    public GameDB gameDB;
    public Player player;
    public EquipmentManager equipmentManager;
    public SkillManager skillManager;
    public PortionManager portionManager;
    public CinematicManager chematicManager;
    public MotionBlurManager motionBlurManager;
    public DialogManager dialogManager;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
