using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStaticData : MonoBehaviour
{

    public int game_money = 0; //데이터베이스 저장 필요
    public float clock_hand_rot = 0;
    public float mainscene_time = 0;

    private void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("GameData"));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
