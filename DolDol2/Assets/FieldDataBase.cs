using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDataBase
{
    public int[,] FieldMap;
    public string[,] FieldMapCSV;
    public int IndexI = 0;
    public int IndexJ = 0;
    public FieldDataBase() {}
    public FieldDataBase(string stage) {}
    public virtual int[,] GetPartialMap(int indexI, int indexJ) 
    {
        return new int[7, 7];
    }
}