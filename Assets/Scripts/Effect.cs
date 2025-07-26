using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private Material faceMaterial;
    [SerializeField] private Color creamColor = new Color(0.9f, 0.8f, 0.7f);

    public void ApplyEffect()
    {
        faceMaterial.color = creamColor;
    }
}
