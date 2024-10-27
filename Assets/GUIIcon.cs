using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIIcon : MonoBehaviour
{

    [SerializeField] private Image itemImage;

    private Sprite currentSprite;
    public void setData(Sprite sprite)
    {
        itemImage.sprite = sprite;
        currentSprite = sprite;
    }

    public Sprite GetSprite()
    {
        return currentSprite;
    }
}
