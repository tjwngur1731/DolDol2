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
    //if (collision.transform.position.y > transform.position.y &&
    //  (collision.transform.position.x >= (transform.position.x - transform.localScale.x/* / 2*/) &&
    //  collision.transform.position.x <= (transform.position.x/* + transform.localScale.x / 2*/)))

    if (collision.transform.position.y > transform.position.y)
    {
      collision.transform.SetParent(transform);
    }
    else
    {
      MovingPlatform parentPlatform = transform.parent.GetComponent<MovingPlatform>();

      parentPlatform.ResetDir(parentPlatform.GetCurrentMovingType());
    }
  }

  void OnCollisionExit2D(Collision2D collision)
  {
    collision.transform.SetParent(null);
  }
}
