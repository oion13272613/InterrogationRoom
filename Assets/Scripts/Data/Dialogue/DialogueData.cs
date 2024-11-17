using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable] // 標記這個類別可以被序列化，以便於保存和編輯
[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Create SO/DialogueData")]
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
