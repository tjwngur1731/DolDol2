using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : BaseObject
{
    Vector2 spawnPos;
    
    
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
                SceneManager.LoadScene("StageSelect");
            }
            else
            {
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
}
