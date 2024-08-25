using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]//可以序列化
[CreateAssetMenu(fileName = "NewObjectData", menuName = "Create SO/ObjectData")]//建立Asset右鍵選單
public class ObjectData : ScriptableObject
{    
    public string objectName;
    [Multiline(5)]//Unity顯示行數
    /// <summary>
    ///descriptionText說明文字
    /// </summary>
    public string descriptionText;
    public int id;

    //從哪個地點來()
    //從哪個人物來(people)
    //圖片(image jpg)

    /// <summary>
    /// 構造器
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
