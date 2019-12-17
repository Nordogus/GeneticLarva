using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private LoadMap map;
    [SerializeField]
    public bool inMove = false;
    public Vector2 direct;
    private Vector2 pos;
    private Vector2 initPos;
    private Vector2 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        map = FindObjectOfType<LoadMap>();
        initPos = transform.position;
    }

    private void Update()
    {
        if (inMove)
        {
            Move();
        }
    }

    public void AffectMove()
    {
        if ((initPos + direct).x < 0 || (initPos + direct).y < 0 || (initPos + direct).x > map.myX - 1 || (initPos + direct).y > map.myY - 1)
        {
            Debug.Log(gameObject.name + " out");
            inMove = false;
            return;
        }
        else
        {
            switch (map.GetContain(new Vector2(transform.position.x + direct.x, transform.position.y + direct.y)).contain)
            {
                case -1:
                    Debug.Log(gameObject.name + " error");
                    break;
                case 0: // empty
                    initPos = transform.position;
                    nextPos = initPos + direct;
                    break;
                case 1: // wall
                    Debug.Log(gameObject.name + " wall");
                    inMove = false;
                    break;
                case 2: // player
                    Debug.Log(gameObject.name + "hit player");
                    inMove = false;
                    break;
                default:
                    Debug.Log(map.GetContain(new Vector2(transform.position.x + direct.y, transform.position.y + direct.y)));
                    inMove = false;
                    break;
            }
        }


    }

    void Move()
    {
        transform.position = Vector2.Lerp(initPos, nextPos, 1);
        if (transform.position.x == nextPos.x && transform.position.y == nextPos.y)
        {
            //map.affectTile(nextPos, map.GetContain(initPos).contain);
            //map.affectTile(initPos, 0);
            initPos = transform.position;
            inMove = false;
        }
    }
}
