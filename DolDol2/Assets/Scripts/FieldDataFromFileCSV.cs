using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class FieldDataFromFileCSV : FieldDataBase
{
  public FieldDataFromFileCSV()
  {
  }

  public FieldDataFromFileCSV(string stage)
  {
    string filename = "CellMap/" + stage + ".csv";
    FieldMapCSV = new string[50 + 2, 50 + 2];

    using (FileStream fs = new FileStream(filename, FileMode.Open))
    {
      using (StreamReader sr = new StreamReader(fs, Encoding.UTF8, false))
      {
        string lines = null;
        string[] values = null;

        int i = 0;
        while ((lines = sr.ReadLine()) != null)
        {
          if (string.IsNullOrEmpty(lines)) return;

          values = lines.Split(',');
          
          for (int j = 0; j < values.Length; j++)
          {
            // Debug.Log(values[j]);
            FieldMapCSV[i + 1, j + 1] = values[j];
          }

          i++;
        }
      }
    }

    int temp = 0;
  }

  public string[,] GetPartialMapCSV(int indexI, int indexJ)
  {
    string[,] resultMap = new string[10 + 2, 10 + 2];

    // if (indexI >= IndexI || indexJ >= IndexJ)
    // {
    //   return null;
    // }

    int intervalI = indexI * 10;
    int intervalJ = indexJ * 10;

    for (int i = 0; i < 10 + 2; i++)
    {
      for (int j = 0; j < 10 + 2; j++)
      {
        resultMap[i, j] = FieldMapCSV[i + intervalI, j + intervalJ];
      }
    }

    return resultMap;
  }
}