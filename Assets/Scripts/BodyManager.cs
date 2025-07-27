using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BodyManager : MonoBehaviour
{
    public static BodyManager main;

    [Header("Objects")]
    [SerializeField] Collider2D faceArea;
    [SerializeField] Image acneImage;
    [SerializeField] Image lipsImage;
    [SerializeField] Image shadowsImage;
    [SerializeField] Image blushImage;

    bool acne = true;
    string lips = "";
    string shadows = "";
    string blush = "";
    float fadeDuration = 1f;
    CanvasGroup acneCanvasGroup;

    void Awake()
    {
        main = this;
        acneCanvasGroup = acneImage.GetComponent<CanvasGroup>();
    }

    public Collider2D GetArea()
    {
        return faceArea;
    }

    public void ApplyingMakeup(Item data)
    {
        if (data.type == ItemType.Cream)
        {
            CreamApply();
        }

        if (data.type == ItemType.Loofah)
        {
            Clear();
        }
    }

    public void Clear()
    {
        lips = "";
        shadows = "";
        blush = "";
        lipsImage.gameObject.SetActive(false);
        shadowsImage.gameObject.SetActive(false);
        blushImage.gameObject.SetActive(false);
    }

    public void CreamApply()
    {
        StartCoroutine(FadeOutAcne());
    }

    IEnumerator FadeOutAcne()
    {
        float elapsedTime = 0f;
        float startAlpha = acneCanvasGroup.alpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            acneCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        acneCanvasGroup.alpha = 0f;
    }
}
