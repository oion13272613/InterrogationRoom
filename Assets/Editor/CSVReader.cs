using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVReader
{
    private static string DialogueDataCSVPath = "/Editor/CSVs/TestExcel.csv";

    [MenuItem("Utilities/GenerateDialogueData")]
   public static void GenerateDialogueData()
    {
        string[] lines = File.ReadAllLines(Application.dataPath+DialogueDataCSVPath);
        foreach (string line in lines) 
        {   //預定項目：如果SO已經被創建的話，只改內容不重創新的
            string[] splitData  = line.Split(',');
            foreach (var item in splitData)
            {
                Debug.Log(item);
            }
        }
        
        Debug.Log("呼叫GenerateDialogueData");
    }


}
