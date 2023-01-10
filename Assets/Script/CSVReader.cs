using System;
using UnityEngine;
using System.IO;
using Debug = System.Diagnostics.Debug;

public class CSVReader : MonoBehaviour
{
    public static void ReadLines(string path, Action<string[], int> callBack)
    {
        var lineNumbers = 0;
        var csvFilePath = "CSV/" + path;
        var csvFile = Resources.Load<TextAsset>(csvFilePath) ;
        Debug.Assert(csvFile != null, nameof(csvFile) + " != null");
        using (var sr = new StringReader(csvFile.text))
        {
            // 末尾まで繰り返す
            while (sr.Peek() > -1)
            {
                // CSVファイルの一行を読み込む
                var lineString = sr.ReadLine();
                // 読み込んだ一行をカンマ毎に分けて配列に格納する
                if (lineString != null)
                {
                    var lineValues = lineString.Split(',');
                    callBack?.Invoke(lineValues, lineNumbers);
                }
        
                lineNumbers++;
            }
        }
    }
     
}