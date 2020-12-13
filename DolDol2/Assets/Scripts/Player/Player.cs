using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : DolObject
{
  Vector2 spawnPos;
  public bool portalContact;

  void Start()
  {
    Init();
  }

  protected override void OnCollisionEnter2D(Collision2D collision)
  {
    base.OnCollisionEnter2D(collision);

    switch (collision.gameObject.tag)
    {
      case "Enemy":
        gameObject.transform.position = spawnPos;
        break;

        //case "Star":
        //  GameManager.Instance.starCount += 1;
        //  break;

        //case "Key":
        //  GameManager.Instance.keyCount += 1;
        //  break;

        //case "Floor":

        //  break;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    switch (collision.gameObject.tag)
    {
      case "Finish":
        Scene scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == 0)      // 메인화면인 경우, 챕터선택화면으로 넘김
        {
          GameObject.Find("Canvas").transform.Find("S").gameObject.SetActive(true);
        }
        else
        {
          portalContact = true;
        }
        break;

      case "Star":
        GameManager.Instance.starCount += 1;
        break;

      case "Key":
        GameManager.Instance.keyCount += 1;
        break;
    }

    //   if (collision.gameObject.tag == "Finish")
    //   {
    //     Scene scene = SceneManager.GetActiveScene();

    //     if (scene.buildIndex == 0)      // 메인화면인 경우, 챕터선택화면으로 넘김
    //     {
    //       SceneManager.LoadScene("ChapterSelect");
    //     }
    //     else if (GameManager.Instance.starCount > 0)
    //     {
    //       StageSelect.clear[StageSelect.chaptNum].stageStar[StageSelect.stageNum - 1] = GameManager.Instance.starCount;
    //       GameManager.Instance.starCount = 0;
    //       if (scene.buildIndex == SceneManager.sceneCount - 1)           // 챕터의 마지막 스테이지인 경우
    //       {
    //         Debug.Log(GameManager.Instance.starCount + " " + scene.buildIndex);
    //         //StageSelect.starCount[scene.buildIndex - 2] = GameManager.Instance.starCount;    // Star count
    //         GameManager.Instance.starCount = 0;
    //         SceneManager.LoadScene("StageSelect");
    //       }
    //       else
    //       {
    //         Debug.Log(GameManager.Instance.starCount + " " + scene.buildIndex);
    //         //GetComponent<StageSelect>().SetStarNum(scene.buildIndex - 2, GameManager.Instance.starCount);    // Star count
    //         //StageSelect.starCount[scene.buildIndex - 2] = GameManager.Instance.starCount;
    //         SceneManager.LoadScene(scene.buildIndex + 1);
    //       }

    //     }
    //     else
    //     {
    //       GameManager.Instance.starCount = 0;
    //       SceneManager.LoadScene(scene.buildIndex);
    //     }

    //   }
    //   else if (collision.gameObject.tag == "Star")
    //   {
    //     GameManager.Instance.starCount += 1;
    //   }
    //   else if (collision.gameObject.tag == "Key")
    //   {
    //     GameManager.Instance.keyCount += 1;
    //   }
    // }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    portalContact = false;
    if (SceneManager.GetActiveScene().buildIndex == 0)
      GameObject.Find("S").gameObject.SetActive(false);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.S) && SceneManager.GetActiveScene().buildIndex == 0)
    {
      if (GameObject.Find("Canvas").transform.Find("S").gameObject.activeSelf == true)
        SceneManager.LoadScene("ChapterSelect");
    }
    else if (Input.GetKeyDown(KeyCode.S) && portalContact == true && GameManager.Instance.starCount > 0)
    {
      Debug.Log("Before " + GameManager.Instance.starCount + " " + ScoreManagement.clear[ScoreManagement.currentChapter - 1].stageStar[ScoreManagement.currentStage - 1]);
      ScoreManagement.clear[ScoreManagement.currentChapter - 1].stageStar[ScoreManagement.currentStage - 1] = GameManager.Instance.starCount;
      GameManager.Instance.starCount = 0;
      if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount - 1)           // 챕터의 마지막 스테이지인 경우
      {
        Debug.Log("After " + GameManager.Instance.starCount + " " + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("StageSelect");
      }
      else
      {
        Debug.Log(GameManager.Instance.starCount + " " + ScoreManagement.clear[ScoreManagement.currentChapter - 1].stageStar[ScoreManagement.currentStage - 1]);
        ScoreManagement.currentStage += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
    }
  }

  public void SetSpawnPos(Vector2 spawnPos)
  {
    this.spawnPos = spawnPos;
  }

  public Vector2 GetSpawnPos()
  {
    return this.spawnPos;
  }
}
