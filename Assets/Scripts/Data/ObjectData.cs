using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // �аO�o�����O�i�H�Q�ǦC�ơA�H�K��O�s�M�s��
[CreateAssetMenu(fileName = "NewObjectData", menuName = "Create SO/ObjectData")] // �b Unity �s�边���k���椤�Ыطs�� ScriptableObject
public class ObjectData : ScriptableObject
{
    
    public string objectName;// ���~�W��

    [Multiline(5)] // �b Unity �s�边����ܦh��奻�ءA��ܦ�Ƭ� 5

    /// <summary>
    /// ���~���y�z��r
    /// </summary>
    public string descriptionText;

    // ���~���ߤ@�ѧO�X
    public int id;

    /// <summary>
    /// �c�y��ơA��l�ƪ��~�� ID �M�W�١C
    /// </summary>
    /// <param name="_id">���~���ߤ@�ѧO�X</param>
    /// <param name="_objectName">���~���W��</param>
    public ObjectData(int _id, string _objectName)
    {
        this.id = _id;                    // �]�m���~���ߤ@�ѧO�X
        this.objectName = _objectName;    // �]�m���~���W��
        this.descriptionText = null;      // �q�{���p�U�y�z��r�� null
    }

    /// <summary>
    /// �����c�y��ơA��l�ƪ��~�� ID�B�W�٩M�y�z��r�C
    /// </summary>
    /// <param name="_id">���~���ߤ@�ѧO�X</param>
    /// <param name="_objectName">���~���W��</param>
    /// <param name="_descriptionText">���~���y�z��r</param>
    public ObjectData(int _id, string _objectName, string _descriptionText)
    {
        this.id = _id;                    // �]�m���~���ߤ@�ѧO�X
        this.objectName = _objectName;    // �]�m���~���W��
        this.descriptionText = _descriptionText; // �]�m���~���y�z��r
    }
}
