using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // �аO�o�����O�i�H�Q�ǦC�ơA�H�K��O�s�M�s��
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
