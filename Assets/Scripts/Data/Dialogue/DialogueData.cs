using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // 標記這個類別可以被序列化，以便於保存和編輯
public class DialogueData
{
    public int characterID;
    public List<string> content;

    public DialogueData()
    {
        characterID = -1;
        content = new List<string>();
    }
}
