using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
    public GameObject dummyObject;

    private Sprite elevatorSprite;
    private Sprite fanSprite;

    private bool containsPointer;
    private Image image;
    private Dictionary<KeyCode, GameObject> keyCodeToPrefab;
    private GameObject placement;
    private GameObject dummyPlacement;
    private GameObject placementPrefab;
    private bool shouldDestroyPlacement = true;

    // Start is called before the first frame update
    void Start()
    {
        elevatorSprite = elevatorPrefab.GetComponent<SpriteRenderer>().sprite;
        fanSprite = fanPrefab.GetComponent<SpriteRenderer>().sprite;

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
        InstantiatePlacementIfNecessary();
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
        ShowSpritePreviewIfNecessary();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image.sprite && image.sprite != defaultSprite)
        {
            image.sprite = defaultSprite;
        }
        containsPointer = false;
        DestroyPreview();
    }

    private void DestroyPlacementIfNecessary()
    {
        if (shouldDestroyPlacement)
        {
            Destroy(placement);
            placement = null;
        }
    }

    private void ShowSpritePreviewIfNecessary()
    {
        if (!dummyPlacement)
        {
            dummyPlacement = Instantiate(dummyObject, transform.position, Quaternion.identity);
            if (placementPrefab == elevatorPrefab)
            {
                dummyPlacement.GetComponent<SpriteRenderer>().sprite = elevatorSprite;
            }
            else
            {
                dummyPlacement.GetComponent<SpriteRenderer>().sprite = fanSprite;
            }
        }
    }

    private void DestroyPreview()
    {
        Destroy(dummyPlacement);
        dummyPlacement = null;
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
            }
        }
    }
}
