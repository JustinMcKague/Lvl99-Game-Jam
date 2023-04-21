using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    private Transform img;

    private void Start()
    {
        img = GetComponent<Transform>();
    }

    public void FlipSpriteX()
    {
        Vector3 scale = img.localScale;
        scale.x *= -1;
        img.localScale = scale;
    }
}
