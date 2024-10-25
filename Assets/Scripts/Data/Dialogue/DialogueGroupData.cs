using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // �аO�o�����O�i�H�Q�ǦC�ơA�H�K��O�s�M�s��
[CreateAssetMenu(fileName = "NewTextData", menuName = "Create SO/TextData")] // �b Unity �s�边���k���椤�Ыطs�� ScriptableObject
public class DialogueGroupData : ScriptableObject
{  
    public TextAsset TextFile; // �x�s�奻�겣���r�q�A�i�H�ΨӸ��J�M��ܤ奻   
    public int textID;// �奻���ߤ@�ѧO�X  
    public int textTrueID;// �ѻP��ܪ����T�ﶵ���ѧO�X   
    public int textFalseID;// �ѻP��ܪ����~�ﶵ���ѧO�X   
    public int ObjectTrueID;// ���~���������T�ѧO�X
    public bool isCorrectText;//�O�_�����T�}��

    /// <summary>
    /// �c�y��ơC�ѩ� ScriptableObject ����ҤƳq�`�� Unity �s�边�����A�o�̪��c�y��ƥi�H�O���q�{��{�C
    /// </summary>
    public DialogueGroupData()
    {
        
    }  
}
