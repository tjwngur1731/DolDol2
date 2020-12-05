using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
  ArrayList collidedDolObjects;
  // Start is called before the first frame update
  void Start()
  {
    collidedDolObjects = new ArrayList();
  }

  // Update is called once per frame
  void Update()
  {
    
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    // int count = collision.contactCount;
    // collidedDolObjects.Add(collision.gameObject.GetComponent<DolObject>());
  }

  void OnCollisionStay2D(Collision2D collision)
  {
    // foreach (DolObject collided in collidedDolObjects)
    // {
    //   if (collided)
    //   {
    //     collided.FixPosition(new Vector3(transform.position.x, collided.transform.position.y, collided.transform.position.z));
    //   }
    // }

    DolObject collided = collision.gameObject.GetComponent<DolObject>();
    
    collided.FixPosition(new Vector3(collision.contacts[0].point.x, collided.transform.position.y, collided.transform.position.z));
      
  }

  void OnCollisionExit2D(Collision2D collision)
  {
    // collidedDolObjects.Remove(collision.gameObject.GetComponent<DolObject>());
  }
}
