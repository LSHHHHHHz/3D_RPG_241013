using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogSystem : MonoBehaviour
{
    private int branch;                     // ���õ� �б�
    [SerializeField] private GameDB dialogDB;              // ��� �����ͺ��̽�
    [SerializeField] private Speaker[] speakers;             // ��ȭ�� �����ϴ� ĳ���͵��� UI �迭
    [SerializeField] private DialogData[] dialogs;           // ���� �б��� ��� ��� �迭

    [SerializeField] private bool isAutoStart = true;        // �ڵ� ���� ����
    private bool isFirst = true;                             // ���� 1ȸ�� ȣ���ϱ� ���� ����
    private int currentDialogIndex = -1;                     // ���� ��� ����
    private int currentSpeakerIndex = 0;                     // ���� ���� �ϴ� ȭ��(Speaker)�� speakers �迭 ����
    [SerializeField] private float typingSpeed = 0.1f;       // �ؽ�Ʈ Ÿ���� ȿ���� ��� �ӵ�
    [SerializeField] private bool isTypingEffect = false;    // �ؽ�Ʈ Ÿ���� ȿ���� ��� ������ ����
    private void OnEnable()
    {
        Setup();
    }
    public void SetBranch(int branch)
    {
        this.branch = branch;
    }
    private void Setup()
    {
        currentDialogIndex = -1;
        currentSpeakerIndex = 0;
        int index = 0;
        for (int i = 0; i < dialogDB.DialogEntites.Count; ++i)
        {
            if (dialogDB.DialogEntites[i].branch == branch)
            {
                dialogs[index].name = dialogDB.DialogEntites[i].name;
                dialogs[index].dialogue = dialogDB.DialogEntites[i].dialog;
                index++;
            }
        }
        // ��� ��ȭ ���� ���ӿ�����Ʈ ��Ȱ��ȭ
        for (int i = 0; i < speakers.Length; ++i)
        {
            SetActiveObjects(speakers[i], false);
        }
    }
    public bool UpdateDialog()
    {
        // ��� �бⰡ ���۵� �� 1ȸ�� ȣ��
        if (isFirst == true)
        {
            // �ʱ�ȭ. ĳ���� �̹����� Ȱ��ȭ�ϰ�, ��� ���� UI�� ��� ��Ȱ��ȭ
            Setup();

            // �ڵ� ��� ������ ������ ù ��° ��� ���
            if (isAutoStart) SetNextDialog();

            isFirst = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            // ��簡 �������� ��� ���� ��� ����
            if (dialogs.Length > currentDialogIndex + 1)
            {
                SetNextDialog();
            }
            // ��簡 �� �̻� ���� ��� ��� ������Ʈ�� ��Ȱ��ȭ�ϰ� true ��ȯ
            else
            {
                for (int i = 0; i < speakers.Length; ++i)
                {
                    SetActiveObjects(speakers[i], false);
                }
                return true;
            }
        }

        return false;
    }
    private void SetNextDialog()
    {
        // ���� ȭ���� ��ȭ ���� ������Ʈ ��Ȱ��ȭ
        SetActiveObjects(speakers[currentSpeakerIndex], false);

        // ���� ��� �����ϵ���
        currentDialogIndex++;

        // ���� ȭ�� ���� ����
        currentSpeakerIndex = dialogs[currentDialogIndex].speakerIndex;

        // ���� ȭ���� ��ȭ ���� ������Ʈ Ȱ��ȭ
        SetActiveObjects(speakers[currentSpeakerIndex], true);

        // ���� ȭ�� �̸� �ؽ�Ʈ ����
        speakers[currentSpeakerIndex].textDialogue.text = "";

        // Ÿ���� ȿ�� ����
        StartCoroutine(OnTypingText());
    }
    private IEnumerator OnTypingText()
    {
        int index = 0;
        isTypingEffect = true;  // Ÿ���� ȿ�� Ȱ��ȭ

        // �ؽ�Ʈ�� �� ���ھ� Ÿ�����Ͽ� ���
        while (index < dialogs[currentDialogIndex].dialogue.Length)
        {
            speakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue.Substring(0, index + 1);
            index++;
            yield return new WaitForSeconds(typingSpeed);  // Ÿ���� �ӵ� ����
        }
        SetDialogueText(dialogs[currentDialogIndex].dialogue);
        isTypingEffect = false;  // Ÿ���� ȿ�� ����

        // ��簡 �Ϸ�Ǿ��� �� ȭ��ǥ ������Ʈ Ȱ��ȭ
        speakers[currentSpeakerIndex].objectArrow.SetActive(true);
    }
    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.imageDialog.gameObject.SetActive(visible);
        speaker.textDialogue.gameObject.SetActive(visible);

        // ȭ��ǥ�� ��簡 ����Ǿ��� ���� Ȱ��ȭ�ϱ� ������ �׻� false
        speaker.objectArrow.SetActive(false);
    }
    private void SetDialogueText(string dialogue)
    {
        speakers[currentSpeakerIndex].textDialogue.text = dialogue;
        speakers[currentSpeakerIndex].textDialogue.rectTransform.sizeDelta = new Vector2(
            speakers[currentSpeakerIndex].imageDialog.rectTransform.sizeDelta.x,
            speakers[currentSpeakerIndex].textDialogue.preferredHeight
        );
    }
}
[System.Serializable]
public struct Speaker
{
    public Image imageDialog;              // ��ȭâ Image UI
    public Text textDialogue;   // ���� ��� ��� Text UI
    public GameObject objectArrow;         // ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� ������Ʈ
}
[System.Serializable]
public struct DialogData
{
    public int speakerIndex;               // �̸��� ��縦 ����� ���� DialogSystem�� speakers �迭 ����
    public string name;                    // ĳ���� �̸�
    [TextArea(3, 5)]
    public string dialogue;                // ���
}
