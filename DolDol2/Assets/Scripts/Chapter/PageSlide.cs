using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSlide : MonoBehaviour
{
    public GameObject scrollBar, Left, Right;
    public GameObject starText, chapterText;

    private void Update()
    {
        if (scrollBar.GetComponent<Scrollbar>().value == 0)
        {
            Left.SetActive(false);
        }
        else if(scrollBar.GetComponent<Scrollbar>().value == 1)
        {
            Right.SetActive(false);
        }
    }
    public void OnClick()       // 좌우 화면 이동
    {
        if (scrollBar.GetComponent<Scrollbar>().value >= 0 && scrollBar.GetComponent<Scrollbar>().value <= 1)
        {
            if (gameObject.name == "Left")
            {
                scrollBar.GetComponent<Scrollbar>().value -= (float)0.3;
                //Debug.Log(scrollBar.GetComponent<Scrollbar>().value);
                Right.SetActive(true);
            }
            else if (gameObject.name == "Right")
            {
                scrollBar.GetComponent<Scrollbar>().value += (float)0.3;
                //Debug.Log(scrollBar.GetComponent<Scrollbar>().value);
                Left.SetActive(true);
            }
            Check();
        }
    }

    void Check()
    {
        if (scrollBar.GetComponent<Scrollbar>().value == 0)
        {
            Left.SetActive(false);
            starText.GetComponent<Text>().text = StageSelect.totalStarCount[0].ToString();
            chapterText.GetComponent<Text>().text = "001";
        }
        else if (scrollBar.GetComponent<Scrollbar>().value > 0 && scrollBar.GetComponent<Scrollbar>().value < 0.4)
        {
            starText.GetComponent<Text>().text = StageSelect.totalStarCount[1].ToString();
            chapterText.GetComponent<Text>().text = "002";
        }
        else if (scrollBar.GetComponent<Scrollbar>().value > 0.4 && scrollBar.GetComponent<Scrollbar>().value < 1)
        {
            starText.GetComponent<Text>().text = StageSelect.totalStarCount[2].ToString();
            chapterText.GetComponent<Text>().text = "003";
        }
        else if (scrollBar.GetComponent<Scrollbar>().value == 1)
        {
            Right.SetActive(false);
            starText.GetComponent<Text>().text = StageSelect.totalStarCount[3].ToString();
            chapterText.GetComponent<Text>().text = "004";
        }
    }
}

/*
*챕터 저장 -> 돌아오면 그 챕터가 뜨도록 하기.
 */
