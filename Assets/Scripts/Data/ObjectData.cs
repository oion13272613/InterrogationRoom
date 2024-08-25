using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]//�i�H�ǦC��
[CreateAssetMenu(fileName = "NewObjectData", menuName = "Create SO/ObjectData")]//�إ�Asset�k����
public class ObjectData : ScriptableObject
{    
    public string objectName;
    [Multiline(5)]//Unity��ܦ��
    /// <summary>
    ///descriptionText������r
    /// </summary>
    public string descriptionText;
    public int id;

    //�q���Ӧa�I��()
    //�q���ӤH����(people)
    //�Ϥ�(image jpg)

    /// <summary>
    /// �c�y��
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_objectName"></param>
    public ObjectData(int _id,string _objectName)
    {
         this.id= _id;
         this.objectName = _objectName;
         this.descriptionText = null;
    }

    public ObjectData(int _id, string _objectName, string _descriptionText)
    {       
        this.id = _id;
        this.objectName = _objectName;
        this.descriptionText = _descriptionText;
    }
}
