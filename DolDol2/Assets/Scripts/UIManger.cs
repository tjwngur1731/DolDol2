using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour
{
  private static UIManger instance = null;
  public GameObject starUI;
  private Image starImage;
  private Sprite[] starImgArr;
  public GameObject stageUI;
  private Text stageText;
  public GameObject choiceUI;
  private Image choiceImage;
  private Sprite[] choiceImgArr;
  public GameObject miniMapUI;
  public GameObject keyUI;
  private Text keyText;
  public GameObject rotateNumberUI;
  private Text rotateNumText;

  void Awake()
  {
    if (null == instance)
    {
      instance = this;
    }
    else
    {
      Destroy(this.gameObject);
    }

    starImage = starUI.GetComponent<Image>();
    stageText = stageUI.GetComponent<Text>();
    choiceImage = choiceUI.GetComponent<Image>();
    keyText = keyUI.GetComponent<Text>();
    rotateNumText = rotateNumberUI.GetComponent<Text>();

    starImgArr = new Sprite[4];

    for (int i = 0; i < 4; i++)
    {
      Object temp = Resources.Load("Play/Image_Star" + i);
      starImgArr[i] = Resources.Load<Sprite>("Play/Image_Star" + i);
    }

    starImage.sprite = starImgArr[0];

    choiceImgArr = new Sprite[2];

    choiceImgArr[0] = Resources.Load<Sprite>("Play/Image_ChooseDori") as Sprite;
    choiceImgArr[1] = Resources.Load<Sprite>("Play/Image_ChooseDuri") as Sprite;

    choiceImage.sprite = choiceImgArr[0];
  }

  public static UIManger Instance
  {
    get
    {
      if (null == instance)
      {
        return null;
      }
      return instance;
    }
  }

  // Start is called before the first frame update
  void Start()
  {
        starImage.sprite = starImgArr[0];
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetStageText(string text)
  {
    stageText.text = text;
  }

  public void SetKeyNumber(int keyCount)
  {
    keyText.text = "x" + keyCount;
  }

  public void SetRotateNum(int rotateNum)
  {
        if (rotateNum > 0)
        {
            rotateNumText.text = rotateNum.ToString();
        }
        else if (rotateNum == 0)
        {
            rotateNumText.text = "X";
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void SetChoiceUI(bool choice)
  {
    if (choice)
    {
      choiceImage.sprite = choiceImgArr[0];
    }
    else
    {
      choiceImage.sprite = choiceImgArr[1];
    }
  }

  public void SetStarUI(int starCount)
  {
    if (starCount > 3)
      return;
      
    starImage.sprite = starImgArr[starCount];
  }
}
