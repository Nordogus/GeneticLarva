using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public bool isAudioEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ModAudio(bool audiostatus)
    {
        isAudioEnabled = audiostatus;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
