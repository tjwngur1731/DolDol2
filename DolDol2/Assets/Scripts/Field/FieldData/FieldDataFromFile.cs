using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FieldDataFromFile : FieldDataBase
{
    public FieldDataFromFile()
    {
    }

    public FieldDataFromFile(string stage)
    {
        string filename = "CellMap/" + stage + ".cellmap";
        string line;

        StreamReader file = new StreamReader(@filename);
        List<string> fieldData = new List<string>();

        
        int idx = 0;
        int jdx = 0;

        while ((line = file.ReadLine()) != null)
        {
            int size = line.Length;
            if (size > jdx) jdx = size;

            fieldData.Add(line);

            idx++;
        }

        int[,] InitMap = new int[idx, jdx];

        for (int i = 0; i < fieldData.Count; i++)
        {
            int size = fieldData[i].Length;

            for (int j = 0; j < size; j++)
            {
                InitMap[i, j] = fieldData[i][j] - '0';
            }
        }

        IndexI = idx / 5;
        IndexJ = jdx / 5;

        if (IndexI <= 0) IndexI = 1;
        if (IndexJ <= 0) IndexJ = 1;

        FieldMap = new int[IndexI * 5 + 2, IndexJ * 5 + 2];

        for (int i = 0; i < IndexI * 5 + 2; i++)
        {
            for (int j = 0; j < IndexJ * 5 + 2; j++)
            {
                FieldMap[i, j] = -1;

                if (i > 0 && i < IndexI * 5 + 1 && j > 0 && j < IndexJ * 5 + 1)
                {
                    FieldMap[i, j] = InitMap[i - 1, j - 1];
                }
            }
        }
    }

    public override int[,] GetPartialMap(int indexI, int indexJ)
    {
        int[,] resultMap = new int[7, 7];

        if (indexI >= IndexI || indexJ >= IndexJ)
        {
            return null;
        }

        int intervalI = indexI * 5;
        int intervalJ = indexJ * 5;

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                resultMap[i, j] = FieldMap[i + intervalI, j + intervalJ];
            }
        }

        return resultMap;
    }
}