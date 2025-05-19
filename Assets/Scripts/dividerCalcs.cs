using UnityEngine;

public class dividerCalcs : MonoBehaviour
{
    public RectTransform splitLineRect;
    public Canvas canvas;
    void Update() {
        float canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        float centerY = canvasHeight / 2f;
        splitLineRect.anchoredPosition = new Vector2(splitLineRect.anchoredPosition.x, centerY);
    }

}
