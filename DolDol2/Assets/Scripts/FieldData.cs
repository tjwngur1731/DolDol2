using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldData
{
    public int[,] FieldMap;

    public FieldData()
    {
        // FieldMap = new int[,] 
        // {
        //     {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        //     {2, ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 2},
        //     {2, ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 2},
        //     {2, ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 2},
        //     {2, ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 2},
        //     {2, ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 2},
        //     {2, ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 2},
        //     {2, ' ', 2, 2, 2, ' ', ' ', ' ', ' ', 2},
        //     {2, ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 2},
        //     {2, 2, 2, 2, 2, 2, 2, 2, 2, 2}
        // };

        FieldMap = new int[,] 
        {
            {2, ' ', ' ', ' ', 2, ' ', ' ', ' ', ' ', ' '},
            {2, ' ', ' ', ' ', 2, ' ', ' ', ' ', ' ', ' '},
            {2, ' ', ' ', ' ', 2, ' ', ' ', ' ', ' ', ' '},
            {2, ' ', ' ', ' ', 2, ' ', ' ', ' ', ' ', ' '},
            {2, 2, 2, 2, 2, ' ', ' ', ' ', ' ', ' '},
            {2, ' ', ' ', ' ', 2, ' ', ' ', ' ', ' ', ' '},
            {2, ' ', ' ', ' ', 2, ' ', ' ', ' ', ' ', ' '},
            {2, ' ', ' ', ' ', 2, ' ', ' ', ' ', ' ', ' '},
            {2, ' ', ' ', ' ', 2, ' ', ' ', ' ', ' ', ' '},
            {2, 2, 2, 2, 2, ' ', ' ', ' ', ' ', ' '},
        };
    }

    public int[,] GetPartialMap(int indexI, int indexJ)
    {
        int[,] resultMap = new int[5,5];

        int intervalI = indexI * 5;
        int intervalJ = indexJ * 5;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                resultMap[i,j] = FieldMap[i + intervalI, j + intervalJ];
            }   
        }

        return resultMap;
    }
}