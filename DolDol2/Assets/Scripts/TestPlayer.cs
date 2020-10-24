using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : BaseObject
{
    // Start is called before the first frame update
    private float Speed = 0.05f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-Speed, 0, 0);
        }

        if ( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Speed, 0, 0);
        }
    }
}
