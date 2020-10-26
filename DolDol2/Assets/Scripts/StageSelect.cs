using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public bool[] StageClear;
    public GameObject[] Lights; // 배경 조명
    public GameObject stageManager;

    void Start()
    {
        StageClear = new bool[3];
        StageClear[0] = true;

        Lights = GameObject.FindGameObjectsWithTag("Light");
        
        for (int i=1; i<StageClear.Length; i++) {
            // if(stageManager.GetComponent<StageClear>().StageClear[i] == false)
            StageClear[i] = false;
        }
    }

  
    void Update()
    {
        foreach (GameObject light in Lights)
        {
            light.transform.Rotate(0, 0, (float)0.5);
        }
    }

    public void OnClickButton(int i) //버튼 클릭하면 이동
    {
        if(StageClear[i] == true)    
            SceneManager.LoadScene(gameObject.name);
    }

    public void OnClickReturn() //버튼 클릭하면 이동
    {
            SceneManager.LoadScene("MainScene");
    }

}
