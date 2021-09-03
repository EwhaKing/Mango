using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Image Orig_Cabinet;
    public Image Orig_Shop;
    public Sprite OrigCabinet;
    public Sprite OrigShop;
    public Sprite ClickCabinet;
    public Sprite ClickShop;
    public GameObject clothButton;
    public GameObject clothPrice;
    public GameObject priceScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickCabinet()
    {
        ButtonSound._buttonInstance.onButtonAudio();
        Orig_Cabinet.sprite = ClickCabinet;
        Orig_Shop.sprite = OrigShop;
    }

    public void OnClickShop()
    {
        ButtonSound._buttonInstance.onButtonAudio();
        Orig_Cabinet.sprite = OrigCabinet;
        Orig_Shop.sprite = ClickShop;
        clothButton.SetActive(true);
        clothPrice.SetActive(true);
    }

    public void OnClickCloth()
    {
        ButtonSound._buttonInstance.onButtonAudio();
        priceScreen.SetActive(true);
    }

    public void OnClickBuyYes()
    {
        ButtonSound._buttonInstance.onButtonAudio();
        priceScreen.SetActive(false);
    }

    public void OnClickBuyNo()
    {
        ButtonSound._buttonInstance.onButtonAudio();
        priceScreen.SetActive(false);
    }
}
