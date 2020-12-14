using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterClear : MonoBehaviour
{
    public GameObject[] chapter = new GameObject[ScoreManagement.chaptNum];
    public static int[] totalStarCount; // 각 챕터별 별 개수 총 합


    void Awake()
    {
        totalStarCount = new int[ScoreManagement.chaptNum];

    }
    void Start()
    {

        chapter[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Chapter/Button_Chapter1_1");
        for (int i = 0; i < ScoreManagement.chaptNum; i++)
        {
            totalStarCount[i] = 0;
        }

        for (int i = 1; i<ScoreManagement.chaptNum; i++)                // 나머지 챕터 잠금
            chapter[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Chapter/Button_Chapter1_0");


        for (int i = 0; i<ScoreManagement.chaptNum; i++)
        {
            GameObject.Find("star/starScore").GetComponent<Text>().text = totalStarCount[0].ToString();
            for (int j = 0; j<20; j++)
            {
               totalStarCount[i] += ScoreManagement.clear[i].stageStar[j];
            }

            if(totalStarCount[i] == 60)
            {
                chapter[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Chapter/Button_Chapter1_3");
                //GameObject.Find("star").transform.Find("Clear").gameObject.SetActive(true);       <- 실행시 윗라인 실행X
            }   // 스테이지 완성 및 별 모두 수집 시
            else if(ScoreManagement.clear[i].stageStar[19] > 0)
            {
                chapter[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Chapter/Button_Chapter1_2");
            }   // 스테이지 완성만 했을 시

                                                                                                        // 다음 챕터 해방하기** (이미지 변경 + 버튼 true)

        }
    }

    
}
