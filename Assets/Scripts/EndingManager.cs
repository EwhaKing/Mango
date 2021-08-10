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

    int lifecnt = 3;
    // Start is called before the first frame update
    void Start()
    {
        lifecnt = 3;
        parent_object = GameObject.Find("Canvas");
        if (GameObject.Find("LifeManager_mini"))
            lifecnt = GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().lifecnt;
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
        //모든 작업 중지
        GameObject soundManager = GameObject.Find("SoundManager");
        GameObject lifemanager_mini = GameObject.Find("LifeManager_mini");
        Destroy(soundManager);
        Destroy(lifemanager_mini);
        GameObject.Find("GameManager").SetActive(false);

        if (TotalMoney.totalMoney < 50) //랭킹 x 게임오버팝업, 우선 50원
        {
            gameoverPopup.SetActive(true);
        }
        else //랭킹 o 랭킹판
        {
            rankingPopup.SetActive(true);
        }
    }

    //닉네임 확인 버튼 -> 누르면 랭킹보드 뜸
    public void onButtonOk()
    {
        nikname = nikname_text.text;
    }
}
