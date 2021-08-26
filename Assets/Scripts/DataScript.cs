using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class Data
{
    public int date;
    
    public void printData()
    {
        Debug.Log("Date: " + date);
    }
}
public class DataScript : MonoBehaviour
{

    public static Data data = new Data();
    void Start()
    {
        PlayerPrefs.SetInt("Story_Start", PlayerPrefs.GetInt("Story_Start", 0));

        if (PlayerPrefs.GetInt("Story_Start") == 0)
        {
            data.date = 0;
            string str = JsonUtility.ToJson(data);
            File.WriteAllText(Application.dataPath + "/Data.json", JsonUtility.ToJson(data));

            string str2 = File.ReadAllText(Application.dataPath + "/Data.json");
            Data data_test = JsonUtility.FromJson<Data>(str2);
            data_test.printData();
        }
        else
        {
            string str = File.ReadAllText(Application.dataPath + "/Data.json");
            data = JsonUtility.FromJson<Data>(str);
            data.printData();
        }

       
    }

    public void DateUp()
    {
        data.date++;
        File.WriteAllText(Application.dataPath + "/Data.json", JsonUtility.ToJson(data));

        string str2 = File.ReadAllText(Application.dataPath + "/Data.json");
        Data data_test = JsonUtility.FromJson<Data>(str2);
        data_test.printData();

    }

    public void DataClear()
    {
        data.date = 0;
        File.WriteAllText(Application.dataPath + "/Data.json", JsonUtility.ToJson(data));

        string str2 = File.ReadAllText(Application.dataPath + "/Data.json");
        Data data_test = JsonUtility.FromJson<Data>(str2);
        data_test.printData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
