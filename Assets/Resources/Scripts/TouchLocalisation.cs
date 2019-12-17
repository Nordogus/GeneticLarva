using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLocalisation
{
    public int touchId;
    public GameObject circle;

    public TouchLocalisation(int newTouchId, GameObject newCircle)
    {
        touchId = newTouchId;
        circle = newCircle;
    }
}
