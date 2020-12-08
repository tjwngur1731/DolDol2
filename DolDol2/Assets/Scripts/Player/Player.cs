using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : DolObject
{
  Vector2 spawnPos;
  //StageSelect stageSelect;

  void Start()
  {
    Init();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Floor")
    {
      //foreach (ContactPoint2D contact in collision.contacts)
      //{
      //  if (contact.point.y > transform.position.y)
      //  {
      //    break;
      //  }
      //}
      // CorrectY();
      return;
    }

    ReleaseY();

    switch (collision.gameObject.tag)
    {
      case "Enemy":
        gameObject.transform.position = spawnPos;
        break;

      case "Star":
        GameManager.Instance.starCount += 1;
        break;

      case "Key":
        GameManager.Instance.keyCount += 1;
        break;

        //case "Floor":

        //  break;
    }
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    ReleaseY();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Finish")
    {
      Scene scene = SceneManager.GetActiveScene();

      if (scene.buildIndex == 0)      // 메인화면인 경우, 챕터선택화면으로 넘김
      {
        SceneManager.LoadScene("ChapterSelect");
      }
      else if (GameManager.Instance.starCount > 0)
      {
        StageSelect.clear[StageSelect.chaptNum].stageStar[StageSelect.stageNum - 1] = GameManager.Instance.starCount;
        GameManager.Instance.starCount = 0;
        if (scene.buildIndex == SceneManager.sceneCount - 1)           // 챕터의 마지막 스테이지인 경우
        {
          Debug.Log(GameManager.Instance.starCount + " " + scene.buildIndex);
          //StageSelect.starCount[scene.buildIndex - 2] = GameManager.Instance.starCount;    // Star count
          GameManager.Instance.starCount = 0;
          SceneManager.LoadScene("StageSelect");
        }
        else
        {
          Debug.Log(GameManager.Instance.starCount + " " + scene.buildIndex);
          //GetComponent<StageSelect>().SetStarNum(scene.buildIndex - 2, GameManager.Instance.starCount);    // Star count
          //StageSelect.starCount[scene.buildIndex - 2] = GameManager.Instance.starCount;
          SceneManager.LoadScene(scene.buildIndex + 1);
        }

      }
      else
      {
        GameManager.Instance.starCount = 0;
        SceneManager.LoadScene(scene.buildIndex);
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
