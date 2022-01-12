using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GoMain : MonoBehaviour
{

    GameStaticData dataScript;
    // Start is called before the first frame update

    private void Awake()
    {
        dataScript = GameObject.Find("GameData").GetComponent<GameStaticData>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickGameRealStart()
    {
        ButtonSound._buttonInstance.onButtonAudio();
        GameStaticData.data.data_cloth = dataScript.baby_custom;
        GameStaticData.data.data_money = dataScript.game_money;
        File.WriteAllText(Application.dataPath + "/GameData.json", JsonUtility.ToJson(GameStaticData.data));
        Invoke("changeScene", 0.5f);

        //Debug.Log("버튼 작동 잘 되니?");
    }

    public void changeScene()
    {
        SceneManager.LoadScene("startScene");
    }
}
