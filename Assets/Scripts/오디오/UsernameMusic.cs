using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsernameMusic : MonoBehaviour
{
    GameObject MusicObject;
    public static AudioSource username_music;

    void Awake()
    {
        if(GameObject.Find("SoundManager") == null)
        {
            MusicObject = GameObject.Find("SoundManager2");
            username_music = MusicObject.GetComponent<AudioSource>();
            username_music.Play();
        }
    }
}
