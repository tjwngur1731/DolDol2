using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScoreManagement : MonoBehaviour
{
    public static int stageNum = 20;
    public static int chaptNum = 4;    // 스테이지,챕터 개수

    public static ScoreManagement instance;

    public static int[] prevStarCount;      // 이전 별 개수 (새로 플레이시 비교 위함)
    public static int currentChapter, currentStage;     // 선택한 챕터, 스테이지 번호

    public static StageClear[] clear;
    static bool[] chaptClear = new bool[chaptNum];
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }


    void OnEnable()
    {
        stageNum = 20;
        prevStarCount = new int[stageNum];
        clear = new StageClear[chaptNum];


        for (int i = 0; i < chaptNum; i++) clear[i] = new StageClear();

    }

    private void Start()
    {
        for (int i = 0; i < stageNum; i++)
        {
            if (clear[currentChapter - 1].stageStar[i] > prevStarCount[i])
                prevStarCount[i] = clear[currentChapter - 1].stageStar[i];
            else
                clear[currentChapter - 1].stageStar[i] = prevStarCount[i];        // 더 많은 별개수 저장

        }

    }
}
