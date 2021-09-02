using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStaticData : MonoBehaviour
{
    //데이터 베이스 저장 필요한 변수들
    public int game_money = 0;
    public List<int> baby_custom = new List<int>(3); //0: 옷정보, 1: 머리정보
    public List<int> baby_clothes = new List<int>(4); //옷을 가지면 해당 인덱스에 1 저장, 아니면 0


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
