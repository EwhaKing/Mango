using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ShopData
{
    public ItemData[] item = new ItemData[4];
}
public class ItemData
{
    public int item_num;
    public string item_name;
    public int item_type;
    public int item_cost;
    public bool own;

    public ItemData()
    {
    }
    public ItemData(int num, string name, int type, int cost, bool own) 
    {
        item_num = num;
        item_name = name;
        item_type = type;
        item_cost = cost;
        this.own = own;
    }

}

public class ShopDataScript : MonoBehaviour
{
    public ShopData sd;

    void Start()
    {
        sd = new ShopData();
        string str = File.ReadAllText(Application.dataPath + "/ShopData.json");
        sd = JsonUtility.FromJson<ShopData>(str);
        Debug.Log(str);
        for (int i = 0; i < sd.item.Length; i++)
        {
            Debug.Log("{NUM: " + sd.item[i].item_num + "  NAME: " + sd.item[i].item_name + "  TYPE: " + sd.item[i].item_type + "  COST: " + sd.item[i].item_cost + "  OWN: " + sd.item[i].own + "}");
        }
    }

    public void BuyItem(int num)
    {
        string str = File.ReadAllText(Application.dataPath + "/ShopData.json");
        sd = JsonUtility.FromJson<ShopData>(str);
        sd.item[num].own = true;
        File.WriteAllText(Application.dataPath + "/ShopData.json", JsonUtility.ToJson(sd));

        string str2 = File.ReadAllText(Application.dataPath + "/ShopData.json");
        ShopData data_test = JsonUtility.FromJson<ShopData>(str2);
        Debug.Log("{NUM: " + sd.item[num].item_num + "  NAME: " + sd.item[num].item_name + "  TYPE: " + sd.item[num].item_type + "  COST: " + sd.item[num].item_cost + "  OWN: " + sd.item[num].own + "}");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
