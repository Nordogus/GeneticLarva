using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bump;
    public AudioClip death_attack;
    public AudioClip wall_destroy;
    public AudioClip death_pique;
    public AudioClip rotator;
    public AudioClip move;
    public OptionManager _optionManager;
    public bool isAudioEnabled;

    // Start is called before the first frame update
    void Start()
    {
        _optionManager = GameObject.Find("OptionManager").GetComponent<OptionManager>();
        isAudioEnabled = _optionManager.isAudioEnabled;
        audioSource = GetComponent<AudioSource>();
        bump = Resources.Load<AudioClip>("Resources/Sounds/bump");
        death_pique = Resources.Load<AudioClip>("Resources/Sounds/death_pique");
        death_attack = Resources.Load<AudioClip>("Resources/Sounds/death_attack");
        wall_destroy = Resources.Load<AudioClip>("Resources/Sounds/wall_destroy");
        move = Resources.Load<AudioClip>("Resources/Sounds/move");
        rotator = Resources.Load<AudioClip>("Resources/Sounds/rotator");
    }

    public void Sound_Bump()
    {
        if (isAudioEnabled)
        {
            audioSource.clip = bump;
            audioSource.Play(0);
        }
    }
    public void Sound_Death_Attack()
    {
        if (isAudioEnabled)
        {
            audioSource.clip = death_attack;
            audioSource.Play(0);
        }
    }
    public void Sound_WallDestroy()
    {
        if (isAudioEnabled)
        {
            audioSource.clip = wall_destroy;
            audioSource.Play(0);
        }
    }
    public void Sound_Death_pique()
    {
        if (isAudioEnabled)
        {
            audioSource.clip = death_pique;
            audioSource.Play(0);
        }
    }
    public void Sound_Rotator()
    {
        if (isAudioEnabled)
        {
            audioSource.clip = rotator;
            audioSource.Play(0);
        }
    }
    public void Sound_Move()
    {
        if (isAudioEnabled)

        {
            audioSource.clip = bump;
            audioSource.Play(0);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
