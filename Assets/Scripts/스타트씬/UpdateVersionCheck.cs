using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using System.IO;
using System;
using UnityEngine.Networking;

public class UpdateVersionCheck : MonoBehaviour
{
    string url_version_num = "https://mango-love.herokuapp.com/api/version";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetVersion());
    }

    IEnumerator GetVersion()
    {
        UnityWebRequest versionnum = UnityWebRequest.Get(url_version_num);
        yield return versionnum.SendWebRequest();

        if (versionnum.isNetworkError)
        {
            Debug.Log(versionnum.error); //오류 시 pass
        }

        else
        {
            string version_num = versionnum.downloadHandler.text;

            if (int.Parse(version_num) != PlayerSettings.Android.bundleVersionCode) //서버의 version_num과 유저의 version code 다르면
            {
                Debug.Log("업데이트 버전 숫자? " + version_num);
                GameObject.Find("update_popup_big").transform.GetChild(0).gameObject.SetActive(true); //강제 업데이트 팝업 띄우면서
                GameObject.Find("update_popup").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "v 1." + version_num + ".0"; //업데이트 버전 숫자 띄움
            }
        }
   
    }

}
