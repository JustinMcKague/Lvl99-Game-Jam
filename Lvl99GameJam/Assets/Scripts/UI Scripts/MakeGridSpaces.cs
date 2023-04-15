using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MakeGridSpaces : MonoBehaviour
{
    [Tooltip("Ranges from 1, a \"small\" (5x10) grid to 3, a \"large\" grid (5x12)")]
    [Range(1f, 3f)]
    public int gridSize;

    public GameObject gridPrefab;

    private int previousGridSize;

    private Transform parent;

    private int startingRows = 5;

    private void OnEnable()
    {
        parent = GetComponent<Transform>();
        previousGridSize = 1;
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.childCount > 0 && previousGridSize != gridSize)
        {
            StartCoroutine(Clear());
        }
    }

    private IEnumerator Clear()
    {
        while (parent.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child != this)
                {
                    DestroyImmediate(child.gameObject);
                }
            }
        }
            yield return new WaitForEndOfFrame();
        CreateGrid();
    }

    private void CreateGrid()
    {
        int childrenToSpawn = 9 * startingRows + (gridSize * startingRows);

        for (int i = 0; i < childrenToSpawn; i++)
        {
            GameObject gridSpace = Instantiate(gridPrefab, parent);
        }
        previousGridSize = gridSize;
    }
}
