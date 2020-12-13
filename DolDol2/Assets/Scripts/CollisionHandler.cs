using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    
  }

  // Update is called once per frame
  void Update()
  {
    
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    for (int i = 0; i < collision.contactCount; i++)
    {
      Debug.Log(collision.contacts[i]);
    }

    float left = transform.position.x - transform.localScale.x / 2;
    float right = transform.position.x + transform.localScale.x / 2;

    //if (collision.transform.position.y > transform.position.y &&
    // (collision.transform.position.x >= (transform.position.x - transform.localScale.x / 2) &&
    // collision.transform.position.x <= (transform.position.x + transform.localScale.x / 2)))

    //if (collision.transform.position.y > transform.position.y &&
    // (collision.transform.position.x >= left &&
    // collision.transform.position.x <= right))

    //// if (collision.transform.position.y > transform.position.y)
    //{
    //  collision.transform.SetParent(transform);
    //}
    //else
    //{
    //  MovingPlatform parentPlatform = transform.parent.GetComponent<MovingPlatform>();

    //  parentPlatform.ResetDir(parentPlatform.GetCurrentMovingType());
    //}

    foreach (ContactPoint2D contact in collision.contacts)
    {
      if (contact.point.y <= transform.position.y &&
        contact.point.x >= left &&
        contact.point.x <= right)
      {
        collision.transform.SetParent(transform);
        return;
      }
    }

    MovingPlatform parentPlatform = transform.parent.GetComponent<MovingPlatform>();

    parentPlatform.ResetDir(parentPlatform.GetCurrentMovingType());
  }

  void OnCollisionExit2D(Collision2D collision)
  {
    collision.transform.SetParent(null);
  }
}
