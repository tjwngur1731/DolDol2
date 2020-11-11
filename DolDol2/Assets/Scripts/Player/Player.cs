using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : BaseObject
{
    Vector2 spawnPos;
    //StageSelect stageSelect;

    Rigidbody2D rigid;
    

    void Start()
    {
      rigid = GetComponent<Rigidbody2D>();
    }

    public override bool Init()
    {
        BaseObjectType = 0;

        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            gameObject.transform.position = spawnPos;
        }
        else if (collision.gameObject.tag == "Star")
        {
            GameManager.Instance.starCount += 1;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            // Next Stage
            Scene scene = SceneManager.GetActiveScene();

            if (scene.buildIndex == 0)
            {
                SceneManager.LoadScene("StageSelect");
            }
            else if (scene.buildIndex == 4)
            {
                Debug.Log(GameManager.Instance.starCount + " " + scene.buildIndex);
                StageSelect.starCount[scene.buildIndex - 2] = GameManager.Instance.starCount;    // Star count
                SceneManager.LoadScene("StageSelect");
            }
            else
            {
                Debug.Log(GameManager.Instance.starCount +" " +scene.buildIndex);
                //GetComponent<StageSelect>().SetStarNum(scene.buildIndex - 2, GameManager.Instance.starCount);    // Star count
                StageSelect.starCount[scene.buildIndex - 2] = GameManager.Instance.starCount;
                SceneManager.LoadScene(scene.buildIndex + 1);
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

    public void SetIsKinematic(bool isKinematic)
    {
      rigid.isKinematic = isKinematic;
    }
}
