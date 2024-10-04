using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // 標記這個類別可以被序列化，以便於保存和編輯
[CreateAssetMenu(fileName = "NewTextData", menuName = "Create SO/TextData")] // 在 Unity 編輯器的右鍵菜單中創建新的 ScriptableObject
public class DialogueGroupData : ScriptableObject
{  
    public TextAsset TextFile; // 儲存文本資產的字段，可以用來載入和顯示文本   
    public int textID;// 文本的唯一識別碼  
    public int textTrueID;// 參與對話的正確選項的識別碼   
    public int textFalseID;// 參與對話的錯誤選項的識別碼   
    public int ObjectTrueID;// 物品對應的正確識別碼
    public bool isCorrectText;//是否為正確腳本

    /// <summary>
    /// 構造函數。由於 ScriptableObject 的實例化通常由 Unity 編輯器完成，這裡的構造函數可以保持默認實現。
    /// </summary>
    public DialogueGroupData()
    {
        
    }  
}
