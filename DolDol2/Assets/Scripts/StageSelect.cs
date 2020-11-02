using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public bool[] StageClear;
    public GameObject stageManager;
    public GameObject[] stage;
    public Sprite[] icon;
    
    public static int[] starCount;
    public static int[] prevStarCount;

    /*public Sprite star0;
    public Sprite star1;
    public Sprite star2;
    public Sprite star3;
    */
    private int stageNum;

    void Start()
    {
        stageNum = 10;
        stage = new GameObject[stageNum];
        starCount = new int[stageNum];
        prevStarCount = new int[stageNum];
        StageClear = new bool[stageNum];
        //icon = new Sprite[4];

        //Canvas canvas;
        // stage = GameObject.FindGameObjectsWithTag("Button");

        for (int i = 0; i < starCount.Length; i++)
        {
            starCount[i] = 0;
        }

        for (int i = 0; i < StageClear.Length; i++)
        {
            if (i == 0) StageClear[i] = true;
            // if(stageManager.GetComponent<StageClear>().StageClear[i] == false)
            else StageClear[i] = false;
        }

        for (int i = 0; i < stageNum; i++)
        {
            if(StageClear[i] == false)
            {
                Debug.Log("Color");
                //stage[i].material. = new Color(160/255, 160/255, 160/255);
            }

            if (starCount[i] > prevStarCount[i])
                prevStarCount[i] = starCount[i];

            //stage[2].image.sprite = icon[2];
                //GetComponent<SpriteRenderer>().sprite = icon[2];
        }
    }
    void OnEnable()
    {
        for (int i = 0; i < StageClear.Length; i++)
        {
            if (i == 0) StageClear[i] = true;
            // if(stageManager.GetComponent<StageClear>().StageClear[i] == false)
            else StageClear[i] = false;
        }

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

    public void OnClickButton(int i) //버튼 클릭하면 이동
    {
        //prevStarCount[i] = starCount[i];   // 스테이지 재시작 시, 기존 별점 기록 (최고점수 반영 위함)

        if (i == 0) 
            SceneManager.LoadScene("Stage1");
        else if(StageClear[i] == true)    
            SceneManager.LoadScene(gameObject.name);
    }

    public void OnClickReturn() //버튼 클릭하면 이동
    {
            SceneManager.LoadScene("MainScene");
    }

    /*public void SetStarNum(int stage, int num)
    {
        starCount[stage] = num;
    }*/
}
