using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    AudioSource audioSource;
    public static ButtonSound _buttonInstance;

    public AudioClip buttonBGM;
    public AudioClip babylaughBGM;

    // Start is called before the first frame update
    void Awake()
    {
        _buttonInstance = this;
    }

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>(); //GameManager 오브젝트
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonAudio()
    {
        audioSource.clip = buttonBGM;
        audioSource.volume = 0.7f;
        audioSource.Play();
    }

    public void onBabyAudio()
    {
        audioSource.clip = babylaughBGM;
        audioSource.Play();
    }

}
