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
    [SerializeField] float fadeOutDuration = 0.3f;
    [SerializeField] float fadeInDuration = 0.3f;

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
        shadowRightCanvasGroup = shadowsImageRight.GetComponent<CanvasGroup>();
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
        }

        if (item.type == ItemType.Blush)
        {
            SetComponent(item, blushImage);
            StartCoroutine(FadeIn(blushCanvasGroup));
        }
    }

    public void ShowLeftShadow()
    {       
        StartCoroutine(FadeIn(shadowLeftCanvasGroup));
    }

    public void ShowRightShadow()
    {
        StartCoroutine(FadeIn(shadowRightCanvasGroup));
    }

    public void HideShadows()
    {
        shadowLeftCanvasGroup.alpha = 0f;
        shadowRightCanvasGroup.alpha = 0f;
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

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeOutDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }

    IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float elapsedTime = 0f;
        canvasGroup.alpha = 0f;

        while (elapsedTime < fadeInDuration)
        {            
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
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
