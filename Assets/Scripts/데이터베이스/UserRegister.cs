using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class UserRegister : MonoBehaviour
{
    string url_user = "https://mango-love.herokuapp.com/api/user/registration";
    public TextMeshProUGUI textUsername;
    public TextMeshProUGUI textError;
    public GameObject nameScreen;
    public GameObject nameBackground;
    public bool isPosting;


    public string username;
    public void post()
    {
        isPosting = true;
        StartCoroutine(PostUser());
    }

    public class User
    {
        public string username;

    }

    IEnumerator PostUser()
    {
        UnityWebRequest request = new UnityWebRequest();
        username = textUsername.text;

        User user = new User
        {
            username = username
        };

        string json = JsonUtility.ToJson(user);
        request = UnityWebRequest.Post(url_user, json);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            textError.text = "<color=red>서버 접속실패</color>" + "\n" + "<color=red>나중에 다시 시도해주세요.</color>";
            Debug.Log(request.error);
        }
        else
        {
            string result = request.downloadHandler.text;
            if(result == "exist")
            {
                textError.text = "<color=red>이미 있는 이름입니다.</color>"+"\n"+ "<color=red>다시 입력하세요</color>";
                isPosting = false;
                Debug.Log("이미 있는 이름입니다. 다시 입력하세요");
            }
            else
            {
                textError.text = "이름 등록 완료!";
                GameStaticData.data.name = username;
                File.WriteAllText(Application.persistentDataPath + "/GameData.json", JsonUtility.ToJson(GameStaticData.data));
                Debug.Log(result + "    " + GameStaticData.data.name);

                Invoke("closeNameScreen", 0.5f);
            }
        }
    }

    void closeNameScreen()
    {
        if(GameObject.Find("SoundManager") != null) StartCoroutine(FadeMusicOutDestroy());
        else StartCoroutine(FadeMusicOut());
    }

    public void onClickOk()
    {
        if(isPosting == false)
        {
            post();
        }
    }

    public void onTextChanged()
    {
        Debug.Log("Text Changed");
        if (textError.text.Contains("이미 있는 이름입니다.") || textError.text.Contains("서버 접속실패"))
        {
            textError.text = "아기의 이름을 지어주세요!";
        }
    }

    IEnumerator FadeMusicOutDestroy()  // 알파값 1 -> 0
    {
        while (StoryMusic.story_music.volume > 0.0f)
        {
            StoryMusic.story_music.volume -= (Time.deltaTime);
            yield return null;
        }
        Destroy(GameObject.Find("SoundManager"));
        SceneManager.LoadScene("StartScene"); //스타트 씬 띄우기
    }

    IEnumerator FadeMusicOut()  // 알파값 1 -> 0
    {
        while (UsernameMusic.username_music.volume > 0.0f)
        {
            UsernameMusic.username_music.volume -= (Time.deltaTime);
            yield return null;
        }
        SceneManager.LoadScene("StartScene"); //스타트 씬 띄우기
    }

    void Start()
    {
        textUsername.text = "";
        isPosting = false;
    }


}
