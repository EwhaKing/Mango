using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStaticData : MonoBehaviour
{
    //데이터 베이스 저장 필요한 변수들
    public int game_money = 0;
    public int baby_custom = 0; //현재 입고있는 옷 세트 번호


    //데이터 베이스 저장 x 변수들
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
