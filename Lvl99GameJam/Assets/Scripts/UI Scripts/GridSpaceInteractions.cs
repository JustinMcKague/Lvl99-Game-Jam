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
    
    public GameObject elevatorPrefab;
    public GameObject fanPrefab;

    private bool containsPointer;
    private Image image;
    private Dictionary<KeyCode, GameObject> keyCodeToPrefab;
    private GameObject placement;
    private GameObject placementPrefab;
    private bool shouldDestroyPlacement = true;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = defaultSprite;
        keyCodeToPrefab = new Dictionary<KeyCode, GameObject>
        {
            [KeyCode.Alpha1] = elevatorPrefab,
            [KeyCode.Alpha2] = fanPrefab,
        };
        placementPrefab = keyCodeToPrefab[KeyCode.Alpha1];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (image.sprite && image.sprite != clickedSprite)
        {
            image.sprite = clickedSprite;
        }
        shouldDestroyPlacement = !shouldDestroyPlacement;
        DestroyPlacementIfNecessary();
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
        containsPointer = true;
        InstantiatePlacementIfNecessary();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image.sprite && image.sprite != defaultSprite)
        {
            image.sprite = defaultSprite;
        }
        containsPointer = false;
        DestroyPlacementIfNecessary();
    }

    private void DestroyPlacementIfNecessary()
    {
        if (shouldDestroyPlacement)
        {
            Destroy(placement);
            placement = null;
        }
    }

    private void InstantiatePlacementIfNecessary()
    {
        if (!placement)
        {
            placement = Instantiate(placementPrefab, transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        foreach (var (keyCode, prefab) in keyCodeToPrefab)
        {
            if (Input.GetKeyDown(keyCode) && placementPrefab != prefab)
            {
                placementPrefab = prefab;
                if (containsPointer)
                {
                    DestroyPlacementIfNecessary();
                    InstantiatePlacementIfNecessary();
                }
            }
        }
    }
}
