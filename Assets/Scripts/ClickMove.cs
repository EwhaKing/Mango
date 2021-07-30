using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour
{
    //1. 건네기 눌렀을 때
    //2. 차가 손님에게 이동
    //3. 손님 사라짐
    //4. 그 자리에 차 값 위로 올라가면서 사라짐

    //책상 위의 tea에 적용해야 할 것 같은데
    //어떻게 버튼을 눌렀을 때 tea의 스크립트가 작동되게 할지?

    //3개 중 2개는 비활성화 상태에서
    //1개에 대해서만 작동되게 하는 부분에 대한 고민

    /*public GameObject targetPosition; //손님 이미지로 설정하기
    public GameObject plusCoin; //코인 이미지로 설정하기
    public float MoneyDelay = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        plusCoin.renderer.enabled = false; //처음에는 코인이 없음
        StartCoroutine("DisplayMoney");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition.transform.position, 0.1f); //사람 object에게 이동
        Destroy(targetPosition, 1); //손님 1초 뒤에 사라짐
        Invoke("MoneyShow", 2); //MoneyShow 함수 2초 뒤에 실행

        //코인 위로 이동하게
        Vector3 pos = transform.position;
        pos.y += 0.001f;
        transform.position = pos;

    }

    void MoneyShow()
    {
        DisplayMoney();
    }

    IEnumerator DisplayMoney()
    {
        yield return new WaitForSeconds(MoneyDelay);

        for (float a = 1; a >= 0; a -= 0.05f)
        {
            transform.plusCoin.material.color = new Vector4(1, 1, 1, a);
            yield return new WaitForFixedUpdate();
        }

        Destroy(plusCoin);
    }*/

    
}
