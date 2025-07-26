using UnityEngine;
using UnityEngine.UI;

public class BodyManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] Collider2D faceArea;
    [SerializeField] GameObject acneObject;
    [SerializeField] Image lipsImage;
    [SerializeField] Image shadowsImage;
    [SerializeField] Image blushImage;

    bool acne = true;
    string lips = "";
    string shadows = "";
    string blush = "";

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
