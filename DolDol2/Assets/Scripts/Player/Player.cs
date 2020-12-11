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
        
        //StageSelect.clear[StageSelect.currentChapter-1].stageStar[StageSelect.currentStage-1] += 1;
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
          GameObject.Find("Canvas").transform.Find("S").gameObject.SetActive(true);
      }
      else
      {
          portalContact = true;
      }
    }
  }
    private void OnTriggerExit2D(Collider2D collision)
    {
        portalContact = false;
        if(SceneManager.GetActiveScene().buildIndex == 0)
         GameObject.Find("S").gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && SceneManager.GetActiveScene().buildIndex == 0) 
        {
            if(GameObject.Find("Canvas").transform.Find("S").gameObject.activeSelf == true)
                SceneManager.LoadScene("ChapterSelect");
        }
        else if(Input.GetKeyDown(KeyCode.S) && portalContact == true && GameManager.Instance.starCount > 0)
        {
            Debug.Log("Before " + GameManager.Instance.starCount+" "+ScoreManagement.clear[ScoreManagement.currentChapter-1].stageStar[ScoreManagement.currentStage-1]);
            ScoreManagement.clear[ScoreManagement.currentChapter-1].stageStar[ScoreManagement.currentStage - 1] = GameManager.Instance.starCount;
            GameManager.Instance.starCount = 0;
            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount - 1)           // 챕터의 마지막 스테이지인 경우
            {
                Debug.Log("After "+GameManager.Instance.starCount + " " + SceneManager.GetActiveScene().buildIndex);
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
