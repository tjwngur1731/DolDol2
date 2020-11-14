using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BaseObject
{
  SpriteRenderer renderer;
  // const int spriteLength = 12;
  // public Sprite[] wallSprite;

  // Start is called before the first frame update
  void Start()
  {
    // if (renderer == null)
    // {
    //   renderer = GetComponent<SpriteRenderer>();
    // }

    BaseObjectType = 2;
  }

  public override bool Init()
  {
    BaseObjectType = 2;

    return true;
  }

  public override int GetBaseObjectType()
  {
    return BaseObjectType;
  }
    
  // Update is called once per frame
  void Update()
  {

  }

  // public void SetWallType(int type)
  // {
  //   if (renderer == null)
  //   {
  //     renderer = GetComponent<SpriteRenderer>();
  //   }

  //   if (type >= 0 && type < spriteLength)
  //   {
  //     renderer.sprite = wallSprite[type];
  //   }
  // }
}
