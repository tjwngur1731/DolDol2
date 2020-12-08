using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : DolObject
{
  private bool correctSwitch = false;
  // Start is called before the first frame update
  void Start()
  {
    Init();
  }

  // Update is called once per frame
  void Update()
  {
    
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    // if (correctSwitch)
    // {
    //   return;
    // }

    // if (collision.gameObject.tag == "Floor")
    // {
    //   // correctSwitch = true;
    //   CorrectY();
    // }
  }

  private void OnCollsionStay2D(Collision collision)
  {
    int temp = 0;
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    correctSwitch = false;
    // ReleaseY();
  }
}
