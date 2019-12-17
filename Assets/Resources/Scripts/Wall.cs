using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private GameObject mygameObject;

    public GameObject particule;

    public void hit()
    {
        mygameObject = Instantiate(particule);
        mygameObject.transform.position = transform.position;
        Destroy(mygameObject, 2);
    }
}
