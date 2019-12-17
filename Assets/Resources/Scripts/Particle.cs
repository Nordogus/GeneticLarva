using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private float myX = 0.3f;
    private float myY = 0.3f;
    public float timeDestroy = 0.5f;
    public Color myColor;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(Random.Range(-myX, myX), (Random.Range(-myY, myY)), 0);
        Destroy(gameObject, timeDestroy);
        GetComponent<SpriteRenderer>().color = myColor;
    }
}
