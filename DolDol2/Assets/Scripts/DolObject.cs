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
  private bool IsGround = false;
  protected float TileInterval = 0.9f;

  protected int MiniFieldIndexI = -1;
  protected int MiniFieldIndexJ = -1;

  protected Rigidbody2D rigid;

  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    // FixYPos();
  }

  protected void FixYPos()
  {
    transform.position = new Vector3(transform.position.x, (float)Math.Round(transform.position.y * 100.0f) * 0.01f, transform.position.z);
    // transform.position.y = Math.Round(transform.position.y * 100.0f) * 0.01f;
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
    // if (IsGround)
    {
      rigid.isKinematic = isKinematic;
      GetComponent<Collider2D>().isTrigger = isKinematic;
    }
    // else
    // {
    //   if (isKinematic)
    //   {
    //     rigid.bodyType = RigidbodyType2D.Static;
    //   }
    //   else
    //   {
    //     rigid.bodyType = RigidbodyType2D.Dynamic;
    //   }

    //   // GetComponent<Collider2D>().isTrigger = isKinematic;
    // }

    if (!(GameManager.Instance.GetCurrentMiniFieldIndexI() == MiniFieldIndexI && 
      GameManager.Instance.GetCurrentMiniFieldIndexJ() == MiniFieldIndexJ))
    {
      return;
    }

    // if (IsGround)
    {
      transform.SetParent(miniFieldTransform);
    }
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

  protected virtual void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Floor")
    {
      float left = transform.position.x - transform.localScale.x / 2;
      float right = transform.position.x + transform.localScale.x / 2;

      foreach (ContactPoint2D contact in collision.contacts)
      {
        if (contact.point.y <= transform.position.y &&
          contact.point.x >= left &&
          contact.point.x <= right)
        {
          IsGround = true;
          break;
        }
      }
    }
  }

  protected virtual void OnCollisionExit2D(Collision2D collision)
  {
    if (IsGround)
    {
      IsGround = false;
    }
  }
}
 