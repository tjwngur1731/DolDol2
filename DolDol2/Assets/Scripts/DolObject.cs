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

  Rigidbody2D rigid;
  Collider2D collider;

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
    collider = GetComponent<Collider2D>();

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

  public void FixDolObject(Transform miniFieldTransform, bool isKinematic)
  {
    rigid.isKinematic = isKinematic;
    collider.isTrigger = isKinematic;

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

  protected void DestoryDolObject()
  {
    Debug.Log("destroy");

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
}
