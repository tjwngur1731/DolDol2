using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{ 
    public GameObject stageManager;
    public GameObject[] stage;

    public static int[] prevStarCount;      // 이전 별 개수 (새로 플레이시 비교 위함)
    public static int chaptNum;     // 선택한 챕터 번호

    public static StageClear[] clear;
    static bool[] chaptClear = new bool[4];     // 4 = 챕터 개수
    public static int stageNum;




    void Start()
    {
        stageNum = 20;
        stage = new GameObject[stageNum];
        //starCount = new int[stageNum];
        prevStarCount = new int[stageNum];
        clear = new StageClear[4];        // 챕터 개수 = 4


        for (int i = 0; i < clear.Length; i++) clear[i] = new StageClear();

        for (int i = 0; i < clear.Length; i++)
        {
            clear[i].stageClear[0] = true;
            for (int j = 1; j < clear[i].stageClear.Length; j++)
            {
                clear[i].stageClear[j] = false;
            }
 
        }

        for (int i = 0; i < clear.Length; i++)
        {
            for (int j = 1; j < clear[i].stageClear.Length; j++)
            {
                if (clear[chaptNum].stageStar[i] > prevStarCount[i])
                    prevStarCount[i] = clear[chaptNum].stageStar[i];
            }
        }
    }


    void OnEnable()
    {

        for (int i = 0; i < stageNum; i++)
        {
            if (clear[chaptNum].stageStar[i] > prevStarCount[i])
                prevStarCount[i] = clear[chaptNum].stageStar[i];
            else
                clear[chaptNum].stageStar[i] = prevStarCount[i];        // 더 많은 개수 저장

            switch (clear[chaptNum].stageStar[i])       // 별개수에 맞게 이미지 변경
            {
                case 0: break;
                case 1: GameObject.Find("Stage" + i.ToString()).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/StageSelect/Button_Star1");
                    break;
                case 2:
                    GameObject.Find("Stage" + i.ToString()).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/StageSelect/Button_Star2");
                    break;
                case 3:
                    GameObject.Find("Stage" + i.ToString()).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/StageSelect/Button_Star3");
                    break;
            }
        }
    }


    void Update()
    {

    }

    

    

    

    /*public void SetStarNum(int stage, int num)
    {
        starCount[stage] = num;
    }*/
}