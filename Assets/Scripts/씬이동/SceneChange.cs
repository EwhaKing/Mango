using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void onLoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void onLoadMainScene()
    {
        SceneManager.LoadScene("maingameScene");
    }

    public void onLoadMinigameScene()
    {
        SceneManager.LoadScene("minigameSceneFinish");
    }

    public void onLoadShopScene()
    {
        SceneManager.LoadScene("shopScene");
    }

    public void onLoadSameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
