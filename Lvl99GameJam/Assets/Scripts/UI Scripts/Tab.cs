using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    public TabGroup tabGroup;

    public List<Graphic> graphics;

    public List<Color> activeColors;
    public List<Color> inactiveColors;

    public bool activeAtStart;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < graphics.Count; i++)
        {
            graphics[i].color = inactiveColors[i];
            if (activeAtStart)
                graphics[i].color = activeColors[i];
        }
    }

}