using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �y�z�@�Ө��⪺�򥻫H���A�]�A ID�B�W�٩M�Ϥ��C
/// </summary>
public class Character : ScriptableObject
{
   
    public int id; // ���⪺�ߤ@�ѧO�X

    // ���⪺ ID �ݩʡA�ȯ�Ū��   
    public int ID
    {
        get { return id; }
    }

    
    public string characterName;// ���⪺�W��   
    public Image characterImage;// ���⪺�Ϥ�

    /// <summary>
    /// �c�y��ơA��l�ƨ��⪺ ID �M�W�١C
    /// </summary>
    /// <param name="_id">���⪺�ߤ@�ѧO�X</param>
    /// <param name="_characterName">���⪺�W��</param>
    public Character(int _id, string _characterName)
    {
        this.id = _id;                 // �]�m���⪺�ߤ@�ѧO�X
        this.characterName = _characterName; // �]�m���⪺�W��
        this.characterImage = null;    // �q�{���p�U����Ϥ��� null
    }
}
