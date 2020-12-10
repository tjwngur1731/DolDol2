using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    public void OnClickChapter(int chapternum)          // 챕터 선택
    {
        StageSelect.chaptNum = chapternum;
        SceneManager.LoadScene("StageSelect");          // 스테이지 선택 화면으로 이동, 챕터 인덱스 넘김
    }

    public void OnClickReturn()         // 뒤로가기
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OnClickStage(int i)                     // 스테이지 선택
    {
        //prevStarCount[i] = starCount[i];   // 스테이지 재시작 시, 기존 별점 기록 (최고점수 반영 위함)

        if (i == 1 || StageSelect.clear[StageSelect.chaptNum].stageClear[i - 1] == true)
        {
            StageSelect.stageNum = i;
            SceneManager.LoadScene(StageSelect.chaptNum.ToString());

            string temp = StageSelect.chaptNum.ToString() + "-" + StageSelect.stageNum;

            GameManager.Instance.SetCurrentStageName(StageSelect.chaptNum.ToString() + "-" + StageSelect.stageNum);
        }

    }

    public void OnOption()
    {
        Time.timeScale = 0;
    }

    public void OffOption()
    {
        Time.timeScale = 1;
    }
}
