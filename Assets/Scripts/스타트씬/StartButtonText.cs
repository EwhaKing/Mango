using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonText : MonoBehaviour
{
    public Text dateStart;
    public List<Sprite> startButton_sprite;
    public GameObject start_button;

    public Image startButton;
    public Image ruleButton;
    public Image shopButton;
    public Image newButton;

    // Start is called before the first frame update
    void Start()
    {
        DateCheck();
        StartCoroutine(FadeTextToFullAlpha2());
    }

    void DateCheck()
    {
        if (GameStaticData.data.date > 0)
        {
            start_button.GetComponent<Image>().sprite = startButton_sprite[1];
            dateStart.text = GameStaticData.data.date.ToString();
            dateStart.transform.gameObject.SetActive(true);
        }
        else
        {
            dateStart.transform.gameObject.SetActive(false);
            start_button.GetComponent<Image>().sprite = startButton_sprite[0];
        }
    }

    //버튼 fade in fade out
    IEnumerator FadeTextToFullAlpha2() // 알파값 0 -> 1
    {
        //Debug.Log("button_view");
        startButton.color = new Color(startButton.color.r, startButton.color.g, startButton.color.b, 0);
        ruleButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, 0);
        shopButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, 0);
        newButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, 0);
        dateStart.color = new Color(dateStart.color.r, dateStart.color.g, dateStart.color.b, 0);

        while (startButton.color.a < 1.0f)
        {
            //Debug.Log("Time_" + startButton.color.a);
            startButton.color = new Color(startButton.color.r, startButton.color.g, startButton.color.b, startButton.color.a + (Time.deltaTime / 2.0f));
            ruleButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, ruleButton.color.a + (Time.deltaTime / 2.0f));

            shopButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, ruleButton.color.a + (Time.deltaTime / 2.0f));
            newButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, ruleButton.color.a + (Time.deltaTime / 2.0f));

            dateStart.color = new Color(dateStart.color.r, dateStart.color.g, dateStart.color.b, ruleButton.color.a + (Time.deltaTime / 2.0f));

            //yield return new WaitForSecondsRealtime(1);
            yield return null;
        }
        yield return null;
        //yield return new WaitForSeconds(5f);

    }
}
