using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    protected float UnitLimit = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(UnitLimit, UnitLimit, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetUnitLimit()
    {
        return UnitLimit;
    }
}
