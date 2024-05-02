using UnityEngine;
using UnityEngine.UI;

public class TextOutline : MonoBehaviour
{
    public Text mainText;
    public Text outlineText;
    public Color outlineColor;

    void Start()
    {
        outlineText.text = mainText.text;
        outlineText.color = outlineColor;

        // Adjust outline text position
        RectTransform rt = outlineText.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(1, -1); // Example offset for outline
    }
}
