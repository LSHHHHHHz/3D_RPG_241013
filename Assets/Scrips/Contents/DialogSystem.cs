using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogSystem : MonoBehaviour
{
    private int branch;                     // 선택된 분기
    [SerializeField] private GameDB dialogDB;              // 대사 데이터베이스
    [SerializeField] private Speaker[] speakers;             // 대화에 참여하는 캐릭터들의 UI 배열
    [SerializeField] private DialogData[] dialogs;           // 현재 분기의 대사 목록 배열

    [SerializeField] private bool isAutoStart = true;        // 자동 시작 여부
    private bool isFirst = true;                             // 최초 1회만 호출하기 위한 변수
    private int currentDialogIndex = -1;                     // 현재 대사 순번
    private int currentSpeakerIndex = 0;                     // 현재 말을 하는 화자(Speaker)의 speakers 배열 순번
    [SerializeField] private float typingSpeed = 0.1f;       // 텍스트 타이핑 효과의 재생 속도
    [SerializeField] private bool isTypingEffect = false;    // 텍스트 타이핑 효과를 재생 중인지 여부
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
        // 모든 대화 관련 게임오브젝트 비활성화
        for (int i = 0; i < speakers.Length; ++i)
        {
            SetActiveObjects(speakers[i], false);
        }
    }
    public bool UpdateDialog()
    {
        // 대사 분기가 시작될 때 1회만 호출
        if (isFirst == true)
        {
            // 초기화. 캐릭터 이미지는 활성화하고, 대사 관련 UI는 모두 비활성화
            Setup();

            // 자동 재생 설정이 있으면 첫 번째 대사 재생
            if (isAutoStart) SetNextDialog();

            isFirst = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            // 대사가 남아있을 경우 다음 대사 진행
            if (dialogs.Length > currentDialogIndex + 1)
            {
                SetNextDialog();
            }
            // 대사가 더 이상 없을 경우 모든 오브젝트를 비활성화하고 true 반환
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
        // 이전 화자의 대화 관련 오브젝트 비활성화
        SetActiveObjects(speakers[currentSpeakerIndex], false);

        // 다음 대사 진행하도록
        currentDialogIndex++;

        // 현재 화자 순번 설정
        currentSpeakerIndex = dialogs[currentDialogIndex].speakerIndex;

        // 현재 화자의 대화 관련 오브젝트 활성화
        SetActiveObjects(speakers[currentSpeakerIndex], true);

        // 현재 화자 이름 텍스트 설정
        speakers[currentSpeakerIndex].textDialogue.text = "";

        // 타이핑 효과 실행
        StartCoroutine(OnTypingText());
    }
    private IEnumerator OnTypingText()
    {
        int index = 0;
        isTypingEffect = true;  // 타이핑 효과 활성화

        // 텍스트를 한 글자씩 타이핑하여 재생
        while (index < dialogs[currentDialogIndex].dialogue.Length)
        {
            speakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue.Substring(0, index + 1);
            index++;
            yield return new WaitForSeconds(typingSpeed);  // 타이핑 속도 조절
        }
        SetDialogueText(dialogs[currentDialogIndex].dialogue);
        isTypingEffect = false;  // 타이핑 효과 종료

        // 대사가 완료되었을 때 화살표 오브젝트 활성화
        speakers[currentSpeakerIndex].objectArrow.SetActive(true);
    }
    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.imageDialog.gameObject.SetActive(visible);
        speaker.textDialogue.gameObject.SetActive(visible);

        // 화살표는 대사가 종료되었을 때만 활성화하기 때문에 항상 false
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
    public Image imageDialog;              // 대화창 Image UI
    public Text textDialogue;   // 현재 대사 출력 Text UI
    public GameObject objectArrow;         // 대사가 완료되었을 때 출력되는 커서 오브젝트
}
[System.Serializable]
public struct DialogData
{
    public int speakerIndex;               // 이름과 대사를 출력할 현재 DialogSystem의 speakers 배열 순번
    public string name;                    // 캐릭터 이름
    [TextArea(3, 5)]
    public string dialogue;                // 대사
}
