using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove2 : MonoBehaviour
{
    
    public GameObject targetPosition; //손님 이미지로 설정하기
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velo = Vector3.zero;
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPosition.transform.position, ref velo, .2f); //사람 object에게 이동
    }

    


}
