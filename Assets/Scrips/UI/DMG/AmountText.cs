using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AmountText : MonoBehaviour
{
    Transform mainCamera;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float fadeSpeed = 1f;
    float duration = 1f;
    TextMesh textMesh;
    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }
    private void OnEnable()
    {
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1);
    }
    private void Start()
    {
        mainCamera = Camera.main.transform;
    }
    public void ShowDamageText(string damageText, Vector3 position)
    {
        transform.position = position;
        textMesh.text = damageText;
        StartCoroutine(ActiveText());
    }

    IEnumerator ActiveText()
    {
        float elapsedTime = 0f;
        Color originalColor = textMesh.color;

        while (elapsedTime < duration)
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);

            float alpha = Mathf.Lerp(originalColor.a, 0, elapsedTime / duration);
            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.rotation * Vector3.forward, mainCamera.rotation * Vector3.up);
    }
}
