using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameShop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickGameShop()
    {
        ButtonSound._buttonInstance.onButtonAudio();

        SceneManager.LoadScene("shopScene");
    }
}
