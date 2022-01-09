using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;

    // Start is called before the first frame update
    void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMusic(int num)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = audioClips[num];
        audio.Play();
    }

    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }

}
