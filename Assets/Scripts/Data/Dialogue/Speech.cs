using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Speech
{
    public Character speaker;
    public List<Sentence> content;
    public bool isRead;

    public Speech(Character _speaker)
    {
        speaker = _speaker;
        content = new List<Sentence>();
        isRead = false;
    }

    public Speech(Character _speaker, List<Sentence> _content)
    {
        speaker = _speaker;
        content = _content;
        isRead = false;
    }
}
