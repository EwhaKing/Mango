using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaFall : MonoBehaviour
{
    public static Vector3 defaultposition = this.transform.position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Drop", 3.0f);
    }

    void Drop()
    {
        this.transform.position = defaultposition;
        transform.Translate(0, -Time.deltaTime, 0); 
    }
}
