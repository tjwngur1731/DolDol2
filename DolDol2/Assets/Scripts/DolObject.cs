using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DolObject : MonoBehaviour
{
  // Start is called before the first frame update
  public bool IsReRotateNeeded = false;
  public bool IsFixNeeded = false;
  public bool IsStaticObject = false;
  protected float TileInterval = 1.8f / 2;

  protected int MiniFieldIndexI = -1;
  protected int MiniFieldIndexJ = -1;

  protected Rigidbody2D rigid;

  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public virtual bool Init()
  {
    rigid = GetComponent<Rigidbody2D>();

    return true;
  }

  public bool GetIsFixNeeded()
  {
    return IsFixNeeded;
  }

  public bool GetIsReRotateNeeded()
  {
    return IsReRotateNeeded;
  }

  public bool GetIsStaticObject()
  {
    return IsStaticObject;
  }

  public int GetMinifieldIndexJ()
  {
    return MiniFieldIndexJ;
  }

  public int GetMinifieldIndexI()
  {
    return MiniFieldIndexI;
  }

  public void CalculateMinifieldIndex()
  {
    MiniFieldIndexJ = (int)((Math.Round(transform.position.x)) / (10 * TileInterval));
    MiniFieldIndexI = (int)((Math.Round(transform.position.y)) / (10 * TileInterval));
  }

  public virtual void FixDolObject(Transform miniFieldTransform, bool isKinematic)
  {
    rigid.isKinematic = isKinematic;
    GetComponent<Collider2D>().isTrigger = isKinematic;

    if (!(GameManager.Instance.GetCurrentMiniFieldIndexI() == MiniFieldIndexI && 
      GameManager.Instance.GetCurrentMiniFieldIndexJ() == MiniFieldIndexJ))
    {
      return;
    }

    transform.SetParent(miniFieldTransform);
  }

  public void ResetRotation()
  {
    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
  }

  public virtual void DestoryDolObject()
  {
    // Debug.Log("destroy");

    GameManager.Instance.ArrCalcIndex.Remove(gameObject.GetComponent<DolObject>());

    if (IsFixNeeded)
    {
      GameManager.Instance.ArrFixNeeded.Remove(gameObject.GetComponent<DolObject>());
    }

    if (IsReRotateNeeded)
    {
      GameManager.Instance.ArrReRotateNeeded.Remove(gameObject.GetComponent<DolObject>());
    }

    Destroy(gameObject);
  }

  public virtual void FixPosition(Vector3 fixPos)
  {
    transform.position = fixPos;
  }
}
 