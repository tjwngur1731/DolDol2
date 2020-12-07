using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : DolObject
{
  // Start is called before the first frame update
  void Start()
  {
    Init();
  }

  // Update is called once per frame
  void Update()
  {
    // FixYPos();
  }

  // private void OnCollisionEnter2D(Collision2D collision)
  // {
  //   if (collision.gameObject.tag == "Floor" && transform.position.y > collision.transform.position.y)
  //   {
  //     rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
  //   }
  // }

  // private void OnCollisionExit2D(Collision2D collision)
  // {
  //   rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
  // }
}
