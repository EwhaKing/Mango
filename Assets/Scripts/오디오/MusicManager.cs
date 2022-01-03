using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    GameObject BackgroundMusic;
    public static AudioSource backmusic;

    // Start is called before the first frame update
    void Start()
    {
       
    }

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

        /*if (GamePause.bgmOnOff == 1)
        {
            //if (backmusic.isPlaying) return;
            //else backmusic.Play();
            backmusic.Play();
            //DontDestroyOnLoad(BackgroundMusic);
        }
        
        else
        {
            backmusic.Pause();
            //DontDestroyOnLoad(BackgroundMusic);
        }*/
        
    }

    public void BackGroundMusicOffButton()
    {
        BackgroundMusic = GameObject.Find("SoundManager");
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        if (backmusic.isPlaying) backmusic.Pause();
        else backmusic.Play();
    }

}
