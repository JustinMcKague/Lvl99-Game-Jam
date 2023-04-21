using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslatePosition : MonoBehaviour
{
    public AnimationCurve curve;

    [Range(0, 3)]
    public float moveTime;

    public float openDelay, closeDelay;

    [Tooltip("(0, 0) is the bottom left of the screen. 0 is the lowest value in screen space, 1 is the center, and 2 is the maximum.")]
    [Range(-1, 3)]
    public float vectorChangeX, vectorChangeY;
    Vector2 originalLocation;

    [Tooltip("Primarily meant for testing animations, rarely used for final scene.")]
    public bool playOnStart;

    public float finalZPos;

    public bool isToggle;

    // Start is called before the first frame update
    void Start()
    {
        originalLocation = transform.position;

        if (playOnStart)
            Translate(false);
    }

    public void Translate(bool enabled)
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        if (enabled)
        {
            LeanTween.move(gameObject, originalLocation, moveTime / 2).setDelay(closeDelay);
        }
        else
        {
            LeanTween.move(gameObject, new Vector3(Screen.width / 2 * vectorChangeX, Screen.height / 2 * vectorChangeY), moveTime).setEase(curve).setDelay(openDelay);
        }
    }

    public void TranslateToggle()
    {
        if ((Vector2)transform.position == originalLocation)
        {
            LeanTween.move(gameObject, new Vector3(Screen.width / 2 * vectorChangeX, Screen.height / 2 * vectorChangeY), moveTime).setEase(curve).setDelay(openDelay);
        }
        else
        {
            LeanTween.move(gameObject, originalLocation, moveTime / 2).setDelay(closeDelay);
        }
    }

    public void TranslateInZAxis(bool enabled)
    {
        if (enabled)
        {
            LeanTween.move(gameObject, new Vector3(originalLocation.x, originalLocation.y, 0), moveTime / 2).setDelay(closeDelay);
        }
        else
        {
            LeanTween.move(gameObject, new Vector3(originalLocation.x, originalLocation.y, finalZPos), moveTime).setEase(curve).setDelay(openDelay);
        }
    }
}