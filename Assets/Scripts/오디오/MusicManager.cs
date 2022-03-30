using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    GameObject BackgroundMusic;
    public static AudioSource backmusic;

    private void Awake()
    {
        BackgroundMusic = GameObject.Find("SoundManager");
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        if (!GameStaticData.data.bgm_muted) //브금이 꺼진 상태에서는 해당 코드 실행 안되도록 (bgm_muted가 true면 브금 꺼진거임)
        {
            if (backmusic.isPlaying) return;
            else
            {
                backmusic.volume = GameStaticData.data.bgm_slider_value;
                backmusic.Play();
                //DontDestroyOnLoad(BackgroundMusic);
            }
        }
        DontDestroyOnLoad(BackgroundMusic);
    }
}
