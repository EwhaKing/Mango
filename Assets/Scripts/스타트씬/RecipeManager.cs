using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{

    public GameObject recipeDetailBg;

    public Sprite DrinkBg;
    public Sprite TeaBg;
    public Sprite SpecialBg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickDrink()
    {
        recipeDetailBg.GetComponent<Image>().sprite = DrinkBg;
    }

    public void OnClickTea()
    {
        recipeDetailBg.GetComponent<Image>().sprite = TeaBg;
    }

    public void OnClickSpecial()
    {
        recipeDetailBg.GetComponent<Image>().sprite = SpecialBg;
    }
}
