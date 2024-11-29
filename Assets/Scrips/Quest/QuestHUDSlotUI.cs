using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestHUDSlotUI : MonoBehaviour
{
    public string slotGaolName;
    public Image clearIconBackground; 
    public Image clearIcon;
    public RectTransform background;
    public Text questProgressText;

    private Vector3 originalPosition;
    private Color originalBackgroundColor;
    private Color originalClearIconBackgroundColor;
    private Color originalIconColor;
    private Color originalTextColor;

    private void Awake()
    {
        originalPosition = background.localPosition;
        originalBackgroundColor = background.GetComponent<Image>().color;
        originalClearIconBackgroundColor = clearIconBackground.color;
        originalIconColor = clearIcon.color;
        originalTextColor = questProgressText.color;
    }
    private void OnEnable()
    {
        background.localPosition = originalPosition;
        background.GetComponent<Image>().color = originalBackgroundColor;
        clearIconBackground.color = originalClearIconBackgroundColor;
        clearIcon.color = originalIconColor;
        questProgressText.color = originalTextColor;

        background.gameObject.SetActive(true);
        clearIconBackground.gameObject.SetActive(true);
        clearIcon.gameObject.SetActive(false);
        questProgressText.gameObject.SetActive(true);
    }
    public void SetQuestData(string quest)
    {
        questProgressText.text = quest;
        slotGaolName = quest;
    }
    public void ClearQuestUI(string goalName)
    {
        if (goalName == slotGaolName)
        {
            StartCoroutine(HandleClearQuest());
        }
    }

    private IEnumerator HandleClearQuest()
    {
        yield return StartCoroutine(ScaleClearIcon());
        yield return StartCoroutine(MoveAndFadeBackground());
        gameObject.SetActive(false); 
    }

    IEnumerator ScaleClearIcon()
    {
        clearIcon.gameObject.SetActive(true);
        float duration = 0.3f;
        Vector3 startScale = Vector3.one * 0.8f;
        Vector3 midScale = Vector3.one * 1.2f;
        Vector3 endScale = Vector3.one;

        float elapsedTime = 0;
        while (elapsedTime < duration / 2)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / (duration / 2);
            clearIcon.rectTransform.localScale = Vector3.Lerp(startScale, midScale, t);
            yield return null;
        }
        elapsedTime = 0; 
        while (elapsedTime < duration / 2)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / (duration / 2);
            clearIcon.rectTransform.localScale = Vector3.Lerp(midScale, endScale, t);
            yield return null;
        }
        clearIcon.rectTransform.localScale = endScale;
    }

    IEnumerator MoveAndFadeBackground()
    {
        yield return new WaitForSeconds(2.5f);
        float moveDuration = 1.0f;
        float fadeDuration = 0.8f;
        float fadeStartTime = 0.1f;

        Vector3 startPosition = background.localPosition;
        Vector3 endPosition = startPosition + new Vector3(30, 0, 0);

        Image backgroundImage = background.GetComponent<Image>();
        Color startBackgroundColor = backgroundImage.color;
        Color endBackgroundColor = startBackgroundColor;
        endBackgroundColor.a = 0;

        Color startClearIconBackgroundColor = clearIconBackground.color;
        Color endClearIconBackgroundColor = startClearIconBackgroundColor;
        endClearIconBackgroundColor.a = 0;

        Color startIconColor = clearIcon.color;
        Color endIconColor = startIconColor;
        endIconColor.a = 0;

        Color startTextColor = questProgressText.color;
        Color endTextColor = startTextColor;
        endTextColor.a = 0;

        float elapsedTime = 0;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime < moveDuration)
            {
                float moveT = elapsedTime / moveDuration;
                background.localPosition = Vector3.Lerp(startPosition, endPosition, moveT);
            }
            if (elapsedTime >= fadeStartTime)
            {
                float fadeT = (elapsedTime - fadeStartTime) / fadeDuration;
                backgroundImage.color = Color.Lerp(startBackgroundColor, endBackgroundColor, fadeT);
                clearIconBackground.color = Color.Lerp(startClearIconBackgroundColor, endClearIconBackgroundColor, fadeT);
                clearIcon.color = Color.Lerp(startIconColor, endIconColor, fadeT);
                questProgressText.color = Color.Lerp(startTextColor, endTextColor, fadeT);
            }

            yield return null;
        }
        background.gameObject.SetActive(false);
        clearIconBackground.gameObject.SetActive(false);
        clearIcon.gameObject.SetActive(false);
        questProgressText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
