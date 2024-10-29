using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private QuickSkillSlotsUI quickSkillSlotsUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            quickSkillSlotsUI.ActivateSlotSkill(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            quickSkillSlotsUI.ActivateSlotSkill(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            quickSkillSlotsUI.ActivateSlotSkill(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            quickSkillSlotsUI.ActivateSlotSkill(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            quickSkillSlotsUI.ActivateSlotSkill(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            quickSkillSlotsUI.ActivateSlotSkill(5);
        }
    }
}