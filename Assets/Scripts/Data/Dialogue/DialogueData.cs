using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Create SO/DialogueData")]
[Serializable] // �аO�o�����O�i�H�Q�ǦC�ơA�H�K��O�s�M�s��
public class DialogueData : ScriptableObject
{
    public List<Speech> speechList;
    public Speech currentspeech;
    //private int speechIndexID;

    public DialogueData()
    {
        speechList = new List<Speech>();
        currentspeech = null;  
    }

    public DialogueData(List<Speech> _speechList)
    {
        speechList = _speechList;
        currentspeech = null;
    }
}
