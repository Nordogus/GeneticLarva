using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructWall : MonoBehaviour
{
    private GameObject mygameObject;

    public int life = 5;
    public GameObject particule;
    public Sprite stepLife5;
    public Sprite stepLife4;
    public Sprite stepLife3;
    public Sprite stepLife2;
    public Sprite stepLife1;

    public void TakeHit()
    {
        switch (life)
        {
            case 5:
                GetComponent<SpriteRenderer>().sprite = stepLife5;
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = stepLife4;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = stepLife3;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = stepLife2;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = stepLife1;
                break;
            default:
                break;
        }
    }

    public void hit()
    {
        mygameObject = Instantiate(particule);
        mygameObject.transform.position = transform.position;
        Destroy(mygameObject, 2);
    }
}
