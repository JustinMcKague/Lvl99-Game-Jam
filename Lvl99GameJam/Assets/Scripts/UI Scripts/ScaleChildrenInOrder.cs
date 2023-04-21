using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChildrenInOrder : MonoBehaviour
{
    private int childIndex = 0;
    public float scaleTime;

    public AnimationCurve curve;

    public bool playOnStart;

    private void Start()
    {
        if (playOnStart)
            ScaleChildren(false);
    }

    public void ScaleChildren(bool enabled)
    {
        childIndex = 0;
        foreach (Transform child in transform)
        {
            if (!enabled)
            {
                LeanTween.scale(child.gameObject, Vector3.one, scaleTime).setEase(curve).setDelay(childIndex * 0.07f);
                childIndex++;
            }
            else
            {
                LeanTween.scale(child.gameObject, Vector3.zero, scaleTime);
            }
        }
    }
}