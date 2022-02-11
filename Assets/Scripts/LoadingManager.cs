using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    float delayTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadStartScene());
    }

    IEnumerator loadStartScene()
    {

        if (!File.Exists(Application.persistentDataPath + "/GameData.json"))
        {
            yield return new WaitForSeconds(delayTime);
            //게임 첫번째 실행일 경우 스토리 씬 띄우기
            SceneManager.LoadScene("storyScene");
        }
        else
        {
            Debug.Log("게임 첫번째 실행 x");
            yield return new WaitForSeconds(delayTime);
            //아닐 경우 스타트 씬 띄우기
            SceneManager.LoadScene("StartScene");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
