using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectStage : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < ScoreManagement.stageNum; i++)
        {
                switch (ScoreManagement.clear[ScoreManagement.currentChapter - 1].stageStar[i])       // 별개수에 맞게 이미지 변경
                {
                    case 0: break;
                    case 1:
                        GameObject.Find("Stage" + (i + 1).ToString()).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/StageSelect/Button_Star1");
                        break;
                    case 2:
                        GameObject.Find("Stage" + (i + 1).ToString()).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/StageSelect/Button_Star2");
                        break;
                    case 3:
                        GameObject.Find("Stage" + (i + 1).ToString()).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/StageSelect/Button_Star3");
                        break;
                }

            if (ScoreManagement.clear[ScoreManagement.currentChapter - 1].stageStar[i] > 0)
            {
                GameObject stageButton = GameObject.Find("Stage" + (i + 2).ToString());
                stageButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/StageSelect/Button_Star0");
                stageButton.GetComponent<Button>().enabled = true;
            }
        }
    }
}
