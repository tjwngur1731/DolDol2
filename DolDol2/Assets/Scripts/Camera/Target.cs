using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.charChoice == true)
        {
            gameObject.transform.position = player1.transform.position;
        }
        else if (GameManager.Instance.charChoice == false)
        {
            gameObject.transform.position = player2.transform.position;
        }
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

}

