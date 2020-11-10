using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    protected float UnitLimit = 0.35f;
    protected int BaseObjectType = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        // transform.localScale = new Vector3(UnitLimit, UnitLimit, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetUnitLimit()
    {
        return UnitLimit;
    }

    public virtual int GetBaseObjectType()
    {
        return BaseObjectType;
    }
}
