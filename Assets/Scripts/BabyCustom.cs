using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabyCustom : MonoBehaviour
{
    bool baby_check = true;

    public List<Sprite> hair;
    public List<Sprite> clothes;

    List<List<Sprite>> custom_sprites = new List<List<Sprite>>(2); //행: 옷 or 머리, 열: 종류

    GameObject babyObject;

    GameStaticData dataScript;

    List<int> custom_data = new List<int>(2);

    private void Awake()
    {
        dataScript = GameObject.Find("GameData").GetComponent<GameStaticData>();
        custom_data = dataScript.baby_custom;
        babyObject = GameObject.Find("baby_object");

        DontDestroyOnLoad(GameObject.Find("BabyCustom"));
    }

    // Start is called before the first frame update
    void Start()
    {
        
        custom_sprites.Add(new List<Sprite>());
        custom_sprites.Add(new List<Sprite>());
        custom_sprites[0] = clothes;
        custom_sprites[1] = hair;

        if (babyObject.activeSelf) changeBabyCustom();
        
    }

    // Update is called once per frame
    void Update()
    {
        babyObject = GameObject.Find("baby_object");

        if (babyObject)
        {
            if (baby_check)
            {
                changeBabyCustom();
                baby_check = false;
            }
        }
        else baby_check = true;
    }

    public void changeBabyCustom()
    {
        babyObject = GameObject.Find("baby_object");
        custom_data = dataScript.baby_custom;
        Debug.Log(custom_data[0] + " " + custom_data[1]);
        for (int i = 0; i < 2; i++)
        {
            babyObject.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = custom_sprites[i][custom_data[i]];
        }
    }
}
