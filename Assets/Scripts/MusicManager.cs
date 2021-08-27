using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    GameObject BackgroundMusic;
    AudioSource backmusic;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBackMusic()
    {
        if (backmusic.isPlaying) backmusic.Pause();
        else backmusic.Play();
    }

    public void OnClickButtonMusic()
    {
        //if (ButtonSound._buttonInstance.onButtonAudio.isPlaying) ButtonSound._buttonInstance.onButtonAudio.Pause();
        //else ButtonSound._buttonInstance.onButtonAudio.Play();
    }
}
