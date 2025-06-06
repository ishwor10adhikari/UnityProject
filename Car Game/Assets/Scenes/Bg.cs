using UnityEngine;
using UnityEngine.UI;

public class SetBackgroundImage : MonoBehaviour
{
    public Image panelImage; // Assign this in the Inspector
    public Sprite backgroundSprite; // Assign your sprite in the Inspector

    void Start()
    {
        if (panelImage != null && backgroundSprite != null)
        {
            panelImage.sprite = backgroundSprite;
            panelImage.SetNativeSize(); // Optional: Adjusts the size to match the sprite
        }
    }
}
