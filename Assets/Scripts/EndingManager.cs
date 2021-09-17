using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    GameObject parent_object;
    public GameObject gameoverPopup;
    public GameObject rankingPopup;
    public Text nikname_text;

    public string nikname;

    AudioSource audioSource;
    public AudioClip gameover_bgm;
    public AudioClip gameclear_bgm;

    int lifecnt = 3;
    // Start is called before the first frame update
    void Start()
    {
        //lifecnt = 3;
        parent_object = GameObject.Find("Canvas");
        //if (GameObject.Find("LifeManager_mini"))
        //    lifecnt = GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().lifecnt;

        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //게임 오버
        if(lifecnt == 0)
        {
            lifecnt = 3;
            Invoke("gameover", 1f);
        }
    }

    void gameover()
    {
        GameObject lifemanager_mini = GameObject.Find("LifeManager_mini");
        Destroy(lifemanager_mini);
        //모든 작업 중지
        GameObject.Find("GameManager").SetActive(false);

        if (TotalMoney.totalMoney < 1000) //랭킹 x 게임오버팝업, 우선 50원, 원래 5000
        {
            //배경음악 멈춤
            GameObject soundManager = GameObject.Find("SoundManager");
            soundManager.GetComponent<AudioSource>().Stop();

            gameoverPopup.SetActive(true);
            //gameover 효과음 재생
            audioSource.clip = gameover_bgm;
            //audioSource.Play();

            if (GamePause.soundOnOff == 1)
            {
                audioSource.Play();
            }
        }
        else //랭킹 o 랭킹판
        {
            rankingPopup.SetActive(true);

            //gameclear 효과음 재생
            audioSource.clip = gameclear_bgm;
            audioSource.Play();
        }
    }

    //닉네임 확인 버튼 -> 누르면 랭킹보드 뜸
    public void onButtonOk()
    {
        ButtonSound._buttonInstance.onButtonAudio();
        nikname = nikname_text.text;
    }
}
