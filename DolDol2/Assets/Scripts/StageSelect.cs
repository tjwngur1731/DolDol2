using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{ 
    public GameObject stageManager;
    public GameObject[] stage;
    public Sprite[] icon;

    public static int[] totalStarCount;     // 각 챕터별 별 개수 총 합
    public static int[] starCount;          // 각 스테이지 별 개수
    public static int[] prevStarCount;      // 이전 별 개수 (새로 플레이시 비교 위함)
    public static int chaptNum;     // 선택한 챕터 번호

    public static StageClear[] clear;
    static bool[] chaptClear = new bool[3];     // 3 = 챕터 개수
    public static int stageNum;




    void Start()
    {
        stageNum = 10;
        totalStarCount = new int[stageNum];
        stage = new GameObject[stageNum];
        starCount = new int[stageNum];
        prevStarCount = new int[stageNum];
        clear = new StageClear[3];        // 챕터 개수 = 3


        for (int i = 0; i < starCount.Length; i++)
        {
            starCount[i] = 0;
            totalStarCount[i] = 0;
        }

        for (int i = 0; i < clear.Length; i++)
        {
            clear[i].stageClear[0] = true;
            for (int j = 1; j < clear[i].stageClear.Length; j++)
                clear[i].stageClear[j] = false;
        }

        for (int i = 0; i < clear.Length; i++)
        {
            for (int j = 1; j < clear[i].stageClear.Length; j++)
            {
                if (chaptClear[i] == false)
                {
                    //Debug.Log("Color");
                    //stage[i].material. = new Color(160/255, 160/255, 160/255);
                }

                if (starCount[i] > prevStarCount[i])
                    prevStarCount[i] = starCount[i];
            }
        }
    }
    void OnEnable()
    {

        for (int i = 0; i < stageNum; i++)
        {
            if (starCount[i] > prevStarCount[i])
                prevStarCount[i] = starCount[i];
            //stage[2].image.sprite = icon[2];
            //stage[i].GetComponent<SpriteRenderer>().sprite = icon[prevStarCount[i]];
        }
    }


    void Update()
    {

    }

    public void OnClickStage(int i) //버튼 클릭하면 이동
    {
        //prevStarCount[i] = starCount[i];   // 스테이지 재시작 시, 기존 별점 기록 (최고점수 반영 위함)

        if (i == 1 || clear[chaptNum].stageClear[i - 1] == true)
        {
            stageNum = i;
            SceneManager.LoadScene(chaptNum.ToString());
        }

    }

    public void OnClickChapter(int chapternum)          // 챕터 선택
    {
        chaptNum = chapternum;
        SceneManager.LoadScene("StageSelect");          // 스테이지 선택 화면으로 이동, 챕터 인덱스 넘김
    }

    public void OnClickReturn() //버튼 클릭하면 이동
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /*public void SetStarNum(int stage, int num)
    {
        starCount[stage] = num;
    }*/
}
