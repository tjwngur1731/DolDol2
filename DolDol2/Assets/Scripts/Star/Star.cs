using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : BaseObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.starCount += 1;
            Destroy(gameObject);
        }
    }
}
