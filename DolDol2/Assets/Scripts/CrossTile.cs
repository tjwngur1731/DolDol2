using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossTile : DolObject
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public override void DestoryDolObject()
  {
    base.DestoryDolObject();

    GameManager.Instance.ArrStaticRotated.Remove(gameObject.GetComponent<CrossTile>());
  }

  protected override void OnCollisionEnter2D(Collision2D collision)
  {
    collision.transform.SetParent(transform);
    collision.gameObject.GetComponent<DolObject>().SetIsOnCrossTile(true);
    GameManager.Instance.ArrStaticRotated.Add(collision.gameObject.GetComponent<Player>());
  }

  protected override void OnCollisionExit2D(Collision2D collision)
  {
    collision.transform.SetParent(null);
    collision.gameObject.GetComponent<DolObject>().SetIsOnCrossTile(false);
    GameManager.Instance.ArrStaticRotated.Remove(collision.gameObject.GetComponent<Player>());
  }
}
