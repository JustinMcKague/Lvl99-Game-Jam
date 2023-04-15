using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridSpaceInteractions : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite defaultSprite;
    public Sprite hoveredSprite;
    public Sprite clickedSprite;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = defaultSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (image.sprite && image.sprite != clickedSprite)
        {
            image.sprite = clickedSprite;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (image.sprite && image.sprite != hoveredSprite)
        {
            image.sprite = hoveredSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image.sprite && image.sprite != hoveredSprite)
        {
            image.sprite = hoveredSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image.sprite && image.sprite != defaultSprite)
        {
            image.sprite = defaultSprite;
        }
    }
}
