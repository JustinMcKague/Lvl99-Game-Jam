using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UniformScale : MonoBehaviour
{
    [Range(0, 3)]
    public float scaleTime;
    public AnimationCurve curve;

    public float openDelay, closeDelay;

    public float multiplier;

    public bool needsScaling;
    [Tooltip("Primarily meant for testing animations, rarely used for final scene.")]
    public bool playOnStart;

    // Start is called before the first frame update
    void Start()
    {
        if (playOnStart)
            Scale(false);
    }

    /// <summary>
    /// Check the box if the object has already been opened and needs to be closed.
    /// </summary>
    /// <param name="enabled"></param>
    public void Scale(bool enabled)
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

        if (enabled)
        {
            LeanTween.scale(gameObject, Vector2.zero, scaleTime / 2).setDelay(closeDelay);
            StartCoroutine(DisableParent());
        }
        else
        {
            LeanTween.scale(gameObject, Vector2.one * multiplier, scaleTime).setEase(curve).setDelay(openDelay);
        }
    }

    IEnumerator DisableParent()
    {
        yield return new WaitForSeconds(scaleTime / 2);
    }
}