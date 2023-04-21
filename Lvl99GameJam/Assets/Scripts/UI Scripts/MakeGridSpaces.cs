using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MakeGridSpaces : MonoBehaviour
{
    public GameObject gridPrefab;


    private Transform parent;

    [Tooltip("How many units across is your level?")]
    public int startingColumns = 18;

    [Tooltip("How many units high is your level?")]
    public int startingRows = 5;

    private void OnEnable()
    {
        parent = GetComponent<Transform>();
        CreateGrid();
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
        int childrenToSpawn = startingRows * startingColumns;

        for (int i = 0; i < childrenToSpawn; i++)
        {
            GameObject gridSpace = Instantiate(gridPrefab, parent);
        }
    }
}
