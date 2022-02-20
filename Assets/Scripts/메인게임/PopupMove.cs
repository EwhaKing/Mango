using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMove : MonoBehaviour
{
    public float move = 10f;
    public float speed = 10f;
    Vector3 cur_pos, previous;

    bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        cur_pos = this.transform.position;

        previous = new Vector3(cur_pos.x, cur_pos.y + move, cur_pos.z);

        this.transform.position = previous;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("현재 위치: " + transform.position.y);
        if(!check && transform.position.y > cur_pos.y)
        {
            this.transform.Translate(new Vector3(0, -speed, 0)); // 떨어지는 동작
        }
        StartCoroutine(waitTime());
        if(!check && transform.position.y < previous.y)
        {
            check = true;
            this.transform.Translate(new Vector3(0, speed, 0)); // 위로 가는 동작
        }

    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(2f);
    }
}
