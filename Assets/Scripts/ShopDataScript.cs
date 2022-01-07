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
    public int item_type;
    public int item_cost;
    public int item_sprite;
    public bool own;

}

public class ShopDataScript : MonoBehaviour
{
    public static ShopData sd;
    void Start()
    {
        string str = File.ReadAllText(Application.dataPath + "/ShopData.json");
        sd = JsonUtility.FromJson<ShopData>("{\"item\":" + str + "}");

        //확인용 출력
        for (int i = 0; i < sd.item.Length; i++)
        {
            Debug.Log("{NUM: " + sd.item[i].item_num + "  NAME: " + sd.item[i].item_name + "  TYPE: " + sd.item[i].item_type + "  COST: " + sd.item[i].item_cost + "  OWN: " + sd.item[i].own + "}");
        }
    }

    public void BuyItem(int num)
    {
        string str = File.ReadAllText(Application.dataPath + "/ShopData.json");
        sd = JsonUtility.FromJson<ShopData>("{\"item\":" + str + "}");
        sd.item[num].own = true;
        File.WriteAllText(Application.dataPath + "/ShopData.json", JsonUtility.ToJson(sd));

        string str2 = File.ReadAllText(Application.dataPath + "/ShopData.json");
        ShopData data_test = JsonUtility.FromJson<ShopData>("{\"item\":" + str2 + "}");

        //확인용 출력
        Debug.Log("{NUM: " + sd.item[num].item_num + "  NAME: " + sd.item[num].item_name + "  TYPE: " + sd.item[num].item_type + "  COST: " + sd.item[num].item_cost + "  OWN: " + sd.item[num].own + "}");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
