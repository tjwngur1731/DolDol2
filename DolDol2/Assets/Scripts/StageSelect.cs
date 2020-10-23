using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

    public void OnClickButton() //버튼 클릭하면 이동
    {
            SceneManager.LoadScene(gameObject.name);
    }

}
