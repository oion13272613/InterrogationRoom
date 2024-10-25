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
        {   //�w�w���ءG�p�GSO�w�g�Q�Ыت��ܡA�u�鷺�e�����зs��
            string[] splitData  = line.Split(',');
            foreach (var item in splitData)
            {
                Debug.Log(item);
            }
        }
        
        Debug.Log("�I�sGenerateDialogueData");
    }


}
