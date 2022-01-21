using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    GameObject BackgroundMusic;
    public static AudioSource backmusic;

    //public Image bgmOnImg;
    //public Image bgmOffImg;

    public static bool bgmMuted = false;

    //[SerializeField] Slider bgmSlider;

    // Start is called before the first frame update
    void Start()
    {
        /*if (!PlayerPrefs.HasKey("bgmMuted"))
        {
            PlayerPrefs.SetInt("bgmMuted", 0);
            bgmLoad();
        }

        if (!PlayerPrefs.HasKey("bgmVolume"))
        {
            PlayerPrefs.SetFloat("bgmVolume", 1);
            bgmLoad();
        }

        else
        {
            bgmLoad();
        }*/

        
        //AudioListener.pause = bgmMuted;
        
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

    
    public void OnClickBGMOn()
    {
        if (bgmMuted == false)
        {
            bgmMuted = true;
            backmusic.Pause();
        }

        else
        {
            bgmMuted = false;
            backmusic.Play();
        }

        //bgmSave();
        //UpdateBgmImg();
        //MusicManager.backmusic.Play();
    }

    /*private void UpdateBgmImg()
    {
        if (bgmMuted == false)
        {
            bgmOnImg.enabled = true;
            bgmOffImg.enabled = false;
        }

        else
        {
            bgmOnImg.enabled = false;
            bgmOffImg.enabled = true;
        }
    }*/

    /*public void changeBgmVolume()
    {
        backmusic.volume = bgmSlider.value;
        //bgmSave();
    }*/

    /*private void bgmLoad()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        bgmMuted = PlayerPrefs.GetInt("bgmMuted") == 1;
    }

    private void bgmSave()
    {
        PlayerPrefs.SetFloat("bgmVolume", bgmSlider.value);
        PlayerPrefs.SetInt("bgmMuted", bgmMuted ? 1 : 0);
    }*/

}
