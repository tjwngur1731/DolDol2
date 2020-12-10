using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSlide : MonoBehaviour
{
    public static int chapterNum = 4; // 챕터 개수

    public GameObject scrollBar, Left, Right;
    public GameObject starText, chapterTitle;
    public GameObject[] chapter = new GameObject[chapterNum];


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

    void Check()      // 챕터 타이틀 변경 + 챕터 버튼 클릭 가능 여부(//처리된것 챕터 개방시 해제하기)
    {
        for(int i = 0; i<chapterNum; i++)
        {
            chapter[i].GetComponent<Button>().enabled = false;
        }
        if (scrollBar.GetComponent<Scrollbar>().value == 0)
        {
            Left.SetActive(false);
            //starText.GetComponent<Text>().text = ChapterClear.totalStarCount[0].ToString();
            chapterTitle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Chapter/Image_Chapter1_Text");
            chapter[0].GetComponent<Button>().enabled = true;
        }
        else if (scrollBar.GetComponent<Scrollbar>().value > 0 && scrollBar.GetComponent<Scrollbar>().value < 0.4)      // 여기부터는 추후 이전 스테이지 별 개수로 나누어 작동하게 변경하기
        {
            //starText.GetComponent<Text>().text = ChapterClear.totalStarCount[1].ToString();
            chapterTitle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Chapter/Image_ChapterLock_Text");
            //chapter[1].GetComponent<Button>().enabled = true;
        }
        else if (scrollBar.GetComponent<Scrollbar>().value > 0.4 && scrollBar.GetComponent<Scrollbar>().value < 1)
        {
            //starText.GetComponent<Text>().text = ChapterClear.totalStarCount[2].ToString();
            chapterTitle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Chapter/Image_ChapterLock_Text");
            //chapter[2].GetComponent<Button>().enabled = true;
        }
        else if (scrollBar.GetComponent<Scrollbar>().value == 1)
        {
            Right.SetActive(false);
            //starText.GetComponent<Text>().text = ChapterClear.totalStarCount[3].ToString();
            chapterTitle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Chapter/Image_ChapterLock_Text");
            //chapter[3].GetComponent<Button>().enabled = true;
        }
    }
}

/*
*챕터 저장 -> 돌아오면 그 챕터가 뜨도록 하기.
 */
