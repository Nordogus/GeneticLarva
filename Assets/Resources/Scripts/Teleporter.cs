using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject sortie;

    private GameObject mygameObject;

    public GameObject particule;

    public void hit()
    {
        mygameObject = Instantiate(particule);
        mygameObject.transform.position = transform.position;
        Destroy(mygameObject, 2);

        mygameObject = Instantiate(particule);
        mygameObject.transform.position = sortie.transform.position;
        Destroy(mygameObject, 2);
    }
}
