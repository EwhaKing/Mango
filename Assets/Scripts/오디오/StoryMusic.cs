using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryMusic : MonoBehaviour
{
    GameObject MusicObject;
    public static AudioSource story_music;

    void Awake()
    {
        MusicObject = GameObject.Find("SoundManager");
        story_music = MusicObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(MusicObject);
    }
}
