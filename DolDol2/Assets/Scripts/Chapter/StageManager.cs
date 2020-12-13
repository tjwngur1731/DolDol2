using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 모든 스테이지에 공통으로 쓰일 기능들 여기 넣어주세요!

public class StageManager : MonoBehaviour
{
    public bool paused = false;     //일시정지

    public GameObject UIOption;     //일시정지창


    void Start()
    {
        
    }

    private void Awake()
    {
    }

    void Update()
    {
        // 일시정지 구현
            if (GameObject.Find("Canvas").transform.Find("Pause/Option_Window").gameObject.activeSelf == false && Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;
            }
            if (paused)
            {
                AudioManager.pastBVol = AudioManager.bgmVol;
                AudioManager.pastSVol = AudioManager.sfxVol;
                UIOption.SetActive(true);
                Time.timeScale = 0;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            else
            {
                UIOption.SetActive(false);
                Time.timeScale = 1;
            }

    }


    public void OnClickContinue()
    {
        paused = !paused;
    }
}
