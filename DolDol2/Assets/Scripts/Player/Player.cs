using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform spawnPos;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            gameObject.transform.position = spawnPos.position;
            Debug.Log("asf");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
