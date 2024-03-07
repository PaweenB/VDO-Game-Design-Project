using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance{get; set;}

    public AudioSource shootingSound1;
    public AudioSource shootingSound2;
    public AudioSource shootingSound3;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound1()
    {
        shootingSound1.Play();
    }

    public void PlayShootingSound2()
    {
        shootingSound2.Play();
    }

    public void PlayShootingSound3()
    {
        shootingSound3.Play();
    }
}