using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void OnClickChapter(int chapternum)          // 챕터 선택
    {
        ScoreManagement.currentChapter = chapternum;
        SceneManager.LoadScene("StageSelect");          // 스테이지 선택 화면으로 이동, 챕터 인덱스 넘김
    }

    public void OnClickReturn()         // 뒤로가기
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OnClickStage(int i)                     // 스테이지 선택
    {
        //prevStarCount[i] = starCount[i];   // 스테이지 재시작 시, 기존 별점 기록 (최고점수 반영 위함)

        if (i == 1 || ScoreManagement.clear[ScoreManagement.currentChapter].stageStar[i - 1] >0)
        {
            ScoreManagement.currentStage = i;
            SceneManager.LoadScene(ScoreManagement.currentChapter.ToString());

            string temp = ScoreManagement.currentChapter.ToString() + "-" + ScoreManagement.currentStage;

            GameManager.Instance.SetCurrentStageName(ScoreManagement.currentChapter.ToString() + "-" + ScoreManagement.currentStage);
        }

    }

    public void OnOption()
    {
        AudioManager.pastBVol = AudioManager.bgmVol;
        AudioManager.pastSVol = AudioManager.sfxVol;
        Time.timeScale = 0;
    }

    public void OffOption()
    {
        Time.timeScale = 1;
    }

    public void OnClickDiscard()
    {
        audioManager.OnClickCancel();
    }
}
