using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]

public class ShopData
{
    public ItemData[] item;
}

[System.Serializable]
public class ItemData
{
    public int item_num;
    public string item_name;
    public int item_cost;
    public bool own;

}

public class ShopDataScript : MonoBehaviour
{
    public static ShopData sd;
    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("ShopData"));

        if (File.Exists(Application.persistentDataPath + "/ShopData.json"))
        {
            Debug.LogWarning("옷파일 있음");
            Debug.Log("파일 주소: " + Application.persistentDataPath + "/ShopData.json");
            LoadShopData();
           

        }
        else
        {
            Debug.LogWarning("옷파일 없음");
            File.Create(Application.persistentDataPath + "/ShopData.json").Close();
            sd = JsonUtility.FromJson<ShopData>("{\"item\":[{\"item_num\":0,\"item_name\":\"기본옷\", \"item_cost\":0,\"own\":true},{\"item_num\":1,\"item_name\":\"꽃무늬옷\", \"item_cost\":20000,\"own\": false},{\"item_num\":2,\"item_name\":\"트레이닝복\", \"item_cost\":25000,\"own\":false},{\"item_num\":3,\"item_name\":\"유치원복\", \"item_cost\":30000,\"own\":false},{\"item_num\":4,\"item_name\":\"멜빵세트\", \"item_cost\":30000,\"own\":false},{\"item_num\":5,\"item_name\":\"피크닉세트\", \"item_cost\":35000,\"own\":false},{\"item_num\":6,\"item_name\":\"탐정세트\", \"item_cost\":35000,\"own\":false},{\"item_num\":7,\"item_name\":\"꿀벌잠옷\", \"item_cost\":40000,\"own\":false},{\"item_num\":8,\"item_name\":\"곰돌이잠옷\", \"item_cost\":40000,\"own\":false},{\"item_num\":9,\"item_name\":\"한복 두루마기\", \"item_cost\":50000,\"own\":false},{\"item_num\":10,\"item_name\":\"한복 치마\", \"item_cost\":50000,\"own\":false}]}");
            File.WriteAllText(Application.persistentDataPath + "/ShopData.json", JsonUtility.ToJson(sd));

            Invoke("LoadShopData", 0.5f);
        }

    }

    public void LoadShopData()
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/ShopData.json");
        sd = JsonUtility.FromJson<ShopData>(str);
        Debug.Log("불러온 옷: " + str);

    }
}
