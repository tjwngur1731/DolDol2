using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseObject
{
    Vector2 spawnPos;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            gameObject.transform.position = spawnPos;
        }
        if (collision.gameObject.tag == "Star")
        {
            GameManager.Instance.starCount += 1;
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
