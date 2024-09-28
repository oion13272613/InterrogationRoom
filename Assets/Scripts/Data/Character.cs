using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 描述一個角色的基本信息，包括 ID、名稱和圖片。
/// </summary>
public class Character : ScriptableObject
{
   
    public int id; // 角色的唯一識別碼

    // 角色的 ID 屬性，僅能讀取   
    public int ID
    {
        get { return id; }
    }

    
    public string characterName;// 角色的名稱   
    public Image characterImage;// 角色的圖片

    /// <summary>
    /// 構造函數，初始化角色的 ID 和名稱。
    /// </summary>
    /// <param name="_id">角色的唯一識別碼</param>
    /// <param name="_characterName">角色的名稱</param>
    public Character(int _id, string _characterName)
    {
        this.id = _id;                 // 設置角色的唯一識別碼
        this.characterName = _characterName; // 設置角色的名稱
        this.characterImage = null;    // 默認情況下角色圖片為 null
    }
}
