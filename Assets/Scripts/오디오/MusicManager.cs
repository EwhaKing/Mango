using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    GameObject BackgroundMusic;
    public static AudioSource backmusic;

    public static bool bgmMuted = false;

    private void Awake()
    {
        BackgroundMusic = GameObject.Find("SoundManager");
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        if (backmusic.isPlaying) return;
        else
        {
            backmusic.Play();
            DontDestroyOnLoad(BackgroundMusic);
        }
    }
}
