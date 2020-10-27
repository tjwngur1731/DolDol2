using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BaseObject
{
  SpriteRenderer renderer;
  const int spriteLength = 12;
  public Sprite[] wallSprite;

  // Start is called before the first frame update
  void Start()
  {
    if (renderer == null)
    {
      renderer = GetComponent<SpriteRenderer>();
    }
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetWallType(int type)
  {
    if (renderer == null)
    {
      renderer = GetComponent<SpriteRenderer>();
    }

    if (type >= 0 && type < spriteLength)
    {
      renderer.sprite = wallSprite[type];
    }
  }
}
