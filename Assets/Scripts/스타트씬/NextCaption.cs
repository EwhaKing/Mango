using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCaption : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickStoryScene()
    {
        GameStory.index++;
        Debug.Log("바뀐값: " + GameStory.index);
    }
}
