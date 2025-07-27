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
        }

        if (item.type == ItemType.Shadows)
        {
            SetComponent(item, shadowsImage);
        }

         if (item.type == ItemType.Blush)
        {
            SetComponent(item, blushImage);
        }
    }

    void SetComponent(Item item, Image image)
    {
        ItemData itemData = ItemDatabase.main.GetItemData(item.type, item.itemName);
        image.gameObject.SetActive(true);
        image.sprite = itemData.image;
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

    public void Clear()
    {
        lips = "";
        shadows = "";
        blush = "";
        lipsImage.gameObject.SetActive(false);
        shadowsImage.gameObject.SetActive(false);
        blushImage.gameObject.SetActive(false);
    }
}
