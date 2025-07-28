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
    [SerializeField] Image shadowsImageLeft;
    [SerializeField] Image shadowsImageRight;
    [SerializeField] Image blushImage;

    float fadeDuration = 0.3f;
    CanvasGroup acneCanvasGroup;
    CanvasGroup lipsCanvasGroup;
    CanvasGroup blushCanvasGroup;
    CanvasGroup shadowLeftCanvasGroup;
    CanvasGroup shadowRightCanvasGroup;

    void Awake()
    {
        main = this;
        acneCanvasGroup = acneImage.GetComponent<CanvasGroup>();
        lipsCanvasGroup = lipsImage.GetComponent<CanvasGroup>();
        blushCanvasGroup = blushImage.GetComponent<CanvasGroup>();
        shadowLeftCanvasGroup = shadowsImageLeft.GetComponent<CanvasGroup>();
        shadowRightCanvasGroup = shadowsImageLeft.GetComponent<CanvasGroup>();
    }

    public Collider2D GetArea()
    {
        return faceArea;
    }

    public void ApplyingMakeup(Item item)
    {
        if (item.type == ItemType.Cream)
        {
            CreamApply();
        }

        if (item.type == ItemType.Loofah)
        {
            Clear();
        }

        if (item.type == ItemType.Lips)
        {
            SetComponent(item, lipsImage);
            StartCoroutine(FadeIn(lipsCanvasGroup));
        }

        if (item.type == ItemType.Shadows)
        {
            SetComponent(item, shadowsImageLeft, shadowsImageRight);
            StartCoroutine(FadeIn(shadowLeftCanvasGroup));
            StartCoroutine(FadeIn(shadowRightCanvasGroup));
        }

        if (item.type == ItemType.Blush)
        {
            SetComponent(item, blushImage);
            StartCoroutine(FadeIn(blushCanvasGroup));
        }
    }

    void SetComponent(Item item, Image image, Image additionalImage = null)
    {
        ItemData itemData = ItemDatabase.main.GetItemData(item.type, item.itemName);

        if (itemData != null)
        {
            image.gameObject.SetActive(true);
            image.sprite = itemData.image;

            if (additionalImage != null)
            {
                additionalImage.gameObject.SetActive(true);
                additionalImage.sprite = itemData.additionalImage;
            }
        }
    }

    public void CreamApply()
    {
        StartCoroutine(FadeOut(acneCanvasGroup));
    }

    IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }

    IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float elapsedTime = 1f;
        float startAlpha = canvasGroup.alpha;
        canvasGroup.alpha = 0f;

        while (elapsedTime > 0f)
        {
            elapsedTime -= Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    public void Clear()
    {
        lipsImage.gameObject.SetActive(false);
        shadowsImageLeft.gameObject.SetActive(false);
        shadowsImageRight.gameObject.SetActive(false);
        blushImage.gameObject.SetActive(false);
    }
}
