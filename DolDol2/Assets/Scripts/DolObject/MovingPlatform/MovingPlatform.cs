using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : DolObject
{
  private GameObject platform;
  private GameObject route;
  private GameObject point0;
  private GameObject point1;

  private bool currentMovingType = true;
  private Vector3 movingDir;
  private Vector3 start;
  private Vector3 dest;
  private bool moveSwitch = true;
  public float speed = 2;

  // Start is called before the first frame update
  void Start()
  {
    Init();
  }

  public override bool Init()
  {
    platform = GameObject.Find("Platform");
    route = GameObject.Find("Route");
    point0 = GameObject.Find("Point0");
    point1 = GameObject.Find("Point1");

    if (platform)
    {
      platform.transform.SetParent(transform);
      rigid = platform.GetComponent<Rigidbody2D>();
    }

    if (point0 && point1)
    {
      point0.transform.SetParent(transform);
      point1.transform.SetParent(transform);

      ResetDir(currentMovingType);
    }

    return true;
  }
  
  public void ResetDir(bool type)
  {
    if (type == true)
    {
      start = point0.transform.position;
      dest = point1.transform.position;
    }
    else if (type == false)
    {
      start = point1.transform.position;
      dest = point0.transform.position;
    }

    movingDir = (dest - start).normalized;
  }

  // Update is called once per frame
  void Update()
  {
    if (moveSwitch != true)
    {
      return;
    }

    if ((dest - platform.transform.position).sqrMagnitude >= 0.01)
    {
      platform.transform.position += movingDir * Time.deltaTime;
    }
    else
    {
      currentMovingType = !currentMovingType;

      ResetDir(!currentMovingType);
    }
  }

  public void SetCurrentMovingType(bool movingType)
  {
    currentMovingType = movingType;
  }

  public bool GetCurrentMovingType()
  {
    return currentMovingType;
  }

  public override void FixDolObject(Transform miniFieldTransform, bool isKinematic)
  {
    platform.GetComponent<Collider2D>().isTrigger = isKinematic;

    if (!(GameManager.Instance.GetCurrentMiniFieldIndexI() == MiniFieldIndexI &&
      GameManager.Instance.GetCurrentMiniFieldIndexJ() == MiniFieldIndexJ))
    {
      return;
    }

    moveSwitch = !isKinematic;
    transform.SetParent(miniFieldTransform);

    ResetDir(currentMovingType);
  }
}

public class CopyOfMovingPlatform : DolObject
{
  private GameObject platform;
  private GameObject route;
  private GameObject point0;
  private GameObject point1;

  private bool currentMovingType = true;
  private Vector3 movingDir;
  private Vector3 start;
  private Vector3 dest;
  private bool moveSwitch = true;
  public float speed = 2;

  // Start is called before the first frame update
  void Start()
  {
    Init();
  }

  public override bool Init()
  {
    platform = GameObject.Find("Platform");
    route = GameObject.Find("Route");
    point0 = GameObject.Find("Point0");
    point1 = GameObject.Find("Point1");

    if (platform)
    {
      platform.transform.SetParent(transform);
      rigid = platform.GetComponent<Rigidbody2D>();
    }

    if (point0 && point1)
    {
      point0.transform.SetParent(transform);
      point1.transform.SetParent(transform);

      ResetDir(currentMovingType);
    }

    return true;
  }

  public void ResetDir(bool type)
  {
    if (type == true)
    {
      start = point0.transform.position;
      dest = point1.transform.position;
    }
    else if (type == false)
    {
      start = point1.transform.position;
      dest = point0.transform.position;
    }

    movingDir = (dest - start).normalized;
  }

  // Update is called once per frame
  void Update()
  {
    if (moveSwitch != true)
    {
      return;
    }

    if ((dest - platform.transform.position).sqrMagnitude >= 0.01)
    {
      platform.transform.position += movingDir * Time.deltaTime;
    }
    else
    {
      currentMovingType = !currentMovingType;

      ResetDir(!currentMovingType);
    }
  }

  public void SetCurrentMovingType(bool movingType)
  {
    currentMovingType = movingType;
  }

  public bool GetCurrentMovingType()
  {
    return currentMovingType;
  }

  public override void FixDolObject(Transform miniFieldTransform, bool isKinematic)
  {
    platform.GetComponent<Collider2D>().isTrigger = isKinematic;

    if (!(GameManager.Instance.GetCurrentMiniFieldIndexI() == MiniFieldIndexI &&
      GameManager.Instance.GetCurrentMiniFieldIndexJ() == MiniFieldIndexJ))
    {
      return;
    }

    moveSwitch = !isKinematic;
    transform.SetParent(miniFieldTransform);

    ResetDir(currentMovingType);
  }
}
