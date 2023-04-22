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

    private int canvasSortingOrder;
    private bool containsPointer;
    private Image image;
    private Dictionary<KeyCode, GameObject> keyCodeToPrefab;
    private GameObject placement;
    private GameObject placementPreview;
    private GameObject placementPrefab;
    private Quaternion rotation = Quaternion.identity;
    private bool shouldDestroyPlacement = true;

    // Start is called before the first frame update
    void Start()
    {
        canvasSortingOrder = GameObject.Find("Canvas").GetComponent<Canvas>().sortingOrder;
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
        //if (!GameManager.Instance.actionInProgress) {
        if (image.sprite && image.sprite != clickedSprite)
        {
            image.sprite = clickedSprite;
        }
        shouldDestroyPlacement = !shouldDestroyPlacement;
        InstantiatePlacementIfNecessary();
        DestroyPlacementIfNecessary();
        //}
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
        if (!placementPreview && !placement)
        {
            placementPreview = Instantiate(dummyObject, transform.position, Quaternion.identity);
            var placementPreviewSpriteRenderer = placementPreview.GetComponent<SpriteRenderer>();
            placementPreviewSpriteRenderer.sortingOrder = canvasSortingOrder + 1;
            if (placementPrefab == elevatorPrefab)
            {
                placementPreview.transform.localScale = elevatorPrefab.transform.localScale;
                placementPreviewSpriteRenderer.sprite = elevatorSprite;
            }
            else
            {
                placementPreview.transform.localScale = fanPrefab.transform.localScale;
                placementPreview.transform.rotation = rotation;
                placementPreviewSpriteRenderer.sprite = fanSprite;
            }
        }
    }

    private void DestroyPreview()
    {
        if (placementPreview)
        {
            Destroy(placementPreview);
            placementPreview = null;
        }
    }

    private void InstantiatePlacementIfNecessary()
    {
        if (!placement)
        {
            placement = Instantiate(placementPrefab, transform.position, Quaternion.identity, GameManager.Instance.rotatorParent);
            if (placementPrefab != elevatorPrefab)
            {
                placement.transform.rotation = rotation;
            }
        }
    }

    private void Update()
    {
        var recreatePreview = false;
        if (Input.GetKeyDown(KeyCode.R))
        {
            recreatePreview = true;
            rotation *= Quaternion.Euler(0, 0, -45);
        }
        foreach (var (keyCode, prefab) in keyCodeToPrefab)
        {
            if (Input.GetKeyDown(keyCode) && placementPrefab != prefab)
            {
                placementPrefab = prefab;
                recreatePreview = true;
            }
        }
        if (containsPointer && recreatePreview)
        {
            DestroyPreview();
            ShowSpritePreviewIfNecessary();
        }
    }
}
