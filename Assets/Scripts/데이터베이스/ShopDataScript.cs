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

    public void LoadShopData()
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/ShopData.json");
        sd = JsonUtility.FromJson<ShopData>(str);
        Debug.Log("불러온 옷: " + str);

    }
}
