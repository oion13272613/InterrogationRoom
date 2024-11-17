using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // 標記這個類別可以被序列化，以便於保存和編輯
[CreateAssetMenu(fileName = "NewObjectData", menuName = "Create SO/ObjectData")] // 在 Unity 編輯器的右鍵菜單中創建新的 ScriptableObject
public class ObjectData : ScriptableObject
{
    
    public string objectName;// 物品名稱

    [Multiline(5)] // 在 Unity 編輯器中顯示多行文本框，顯示行數為 5

    /// <summary>
    /// 物品的描述文字
    /// </summary>
    public string descriptionText;

    // 物品的唯一識別碼
    public int id;

    /// <summary>
    /// 構造函數，初始化物品的 ID 和名稱。
    /// </summary>
    /// <param name="_id">物品的唯一識別碼</param>
    /// <param name="_objectName">物品的名稱</param>
    public ObjectData(int _id, string _objectName)
    {
        this.id = _id;                    // 設置物品的唯一識別碼
        this.objectName = _objectName;    // 設置物品的名稱
        this.descriptionText = null;      // 默認情況下描述文字為 null
    }

    /// <summary>
    /// 重載構造函數，初始化物品的 ID、名稱和描述文字。
    /// </summary>
    /// <param name="_id">物品的唯一識別碼</param>
    /// <param name="_objectName">物品的名稱</param>
    /// <param name="_descriptionText">物品的描述文字</param>
    public ObjectData(int _id, string _objectName, string _descriptionText)
    {
        this.id = _id;                    // 設置物品的唯一識別碼
        this.objectName = _objectName;    // 設置物品的名稱
        this.descriptionText = _descriptionText; // 設置物品的描述文字
    }
}
