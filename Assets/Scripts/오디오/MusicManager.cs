using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    GameObject BackgroundMusic;
    public static AudioSource backmusic;

    public static bool bgmMuted = false; //브금이 틀어진 상태

    private void Awake()
    {
        BackgroundMusic = GameObject.Find("SoundManager");
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        if (!bgmMuted) //브금이 꺼진 상태에서는 해당 코드 실행 안되도록 (bgmMuted가 true면 브금 꺼진거임)
        {
            if (backmusic.isPlaying) return;
            else
            {
                backmusic.Play();
                DontDestroyOnLoad(BackgroundMusic);
            }
        }
    }
}
