using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1920, 1080, false);

    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }



    public void OnClickStartButton() //[임시] 게임 시작 버튼 클릭하면 이동 -> 게임 오브젝트 생성시 닿으면 이동하게 바꿀예정입니다
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void OnClickEnd()       // 게임 종료버튼
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); //게임 종료
#endif
    }
}
