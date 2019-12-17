using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouches : MonoBehaviour
{
    public GameObject circle;
    public List<TouchLocalisation> touches = new List<TouchLocalisation>();

    public GameObject[] players = new GameObject[2];

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                // 1
                touches.Add(new TouchLocalisation(t.fingerId, createCircle(t)));
            }
            else if (t.phase == TouchPhase.Ended)
            {
                // 1
                TouchLocalisation thisTouch = touches.Find(TouchLocalisation => TouchLocalisation.touchId == t.fingerId);
                Swap(thisTouch.circle.GetComponent<Circle>().initPos, getTouchPosition(t.position));
                Destroy(thisTouch.circle);
                touches.RemoveAt(touches.IndexOf(thisTouch));
                
            }
            else if (t.phase == TouchPhase.Moved)
            {
                // 1
                TouchLocalisation thisTouch = touches.Find(TouchLocalisation => TouchLocalisation.touchId == t.fingerId);
                thisTouch.circle.transform.position = getTouchPosition(t.position);
                if (thisTouch.circle.GetComponent<Circle>().initPos == new Vector2(0, 0))
                {
                    thisTouch.circle.GetComponent<Circle>().initPos = getTouchPosition(t.position);
                }
            }
            i++;
        }
        Vector2 getTouchPosition(Vector2 touchPosition)
        {
            return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
        }

        GameObject createCircle(Touch t)
        {
            GameObject c = Instantiate(circle) as GameObject;
            c.name = "Touch" + t.fingerId;
            c.transform.position = t.position;
            return c;
        }
    }

    public void Swap(Vector2 init, Vector2 end)
    {
        int playerNum;

        if (init.y < Camera.main.transform.position.y)
        {
            playerNum = 0;
        }
        else
        {
            playerNum = 1;
        }

        if (Mathf.Abs(init.y - end.y) < Mathf.Abs(init.x - end.x))
        {
            if (init.x < end.x)
            {
                // right
                players[playerNum].GetComponent<Player>().SwapMove(2);
            }
            else
            {
                // left
                players[playerNum].GetComponent<Player>().SwapMove(3);
            }
        }
        else
        {
            if (init.y < end.y)
            {
                // up
                players[playerNum].GetComponent<Player>().SwapMove(0);
            }
            else
            {
                // dawn
                players[playerNum].GetComponent<Player>().SwapMove(1);
            }
        }
    }
}