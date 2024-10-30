using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSkillSlotUI : BaseSlotUI, IDropHandler
{
    Player player;
    [SerializeField] Image coolDownImage;
    float coolTime;
    bool isOnCooldown = false;
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (!string.IsNullOrEmpty(currentSlotData.dataID))
            {
                ClickButton(currentSlotData.dataID);
            }
        });
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        coolDownImage.fillAmount = 0;
    }
    private void OnEnable()
    {
        onSetData += GameManager.instance.skillManager.SetSkill;
    }
    private void OnDisable()
    {
        onSetData -= GameManager.instance.skillManager.SetSkill;
    }
    public void ActivateSlotSkill()
    {
        if (!string.IsNullOrEmpty(currentSlotData.dataID))
        {
            ClickButton(currentSlotData.dataID);           
        }
    }
    void ClickButton(string id)
    {
        if (isOnCooldown)
        {
            return;
        }

        if (!string.IsNullOrEmpty(id))
        {
            var skill = GameManager.instance.skillManager.GetSkill(id);
            if (skill != null)
            {
                skill.ExcuteSkill(player);
                StartCooldown(id);
                EventManager.instance.ActiveSkill(id);
            }
            else
            {
                Debug.Log("스킬 없음");
            }
        }
    }
    void StartCooldown(string id)
    {
        GameDBEntity db = GameManager.instance.gameDB.GetProfileDB(id);
        coolTime = db.coolDown;
        button.interactable = false;
        StartCoroutine(CooldownCoroutine());
    }
    private IEnumerator CooldownCoroutine()
    {
        isOnCooldown = true;
        float elapsedTime = 0f;

        while (elapsedTime < coolTime)
        {
            elapsedTime += Time.deltaTime;
            coolDownImage.fillAmount = 1 - (elapsedTime / coolTime);
            yield return null;
        }

        coolDownImage.fillAmount = 0;
        isOnCooldown = false;
        button.interactable = true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        DragSlotUI draggedSlot = eventData.pointerDrag.GetComponent<DragSlotUI>();
        if (string.IsNullOrEmpty(draggedSlot.dragDataId))
        {
            return;
        }
        GameDBEntity db = GameManager.instance.gameDB.GetProfileDB(draggedSlot.dragDataId);
        if (IsPossibleDrop(db.dataType) == false)
        {
            return;
        }
        for (int i = 0; i < GameData.instance.quickSkillSlotsData.slotDatas.Count; i++)
        {
            if (db.dataID == GameData.instance.quickSkillSlotsData.slotDatas[i].dataID)
            {
                return;
            }
        }
        currentSlotData.SetData(db.dataID, 0);
    }
}
