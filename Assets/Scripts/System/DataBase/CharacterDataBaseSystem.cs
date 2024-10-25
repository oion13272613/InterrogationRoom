using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色資料庫系統，負責管理角色資料庫中的角色清單
/// </summary>
public class CharacterDataBaseSystem : MonoSingleton<CharacterDataBaseSystem>
{
    // 角色資料庫清單，儲存所有的角色資訊
    public List<Character> CharacterDBList = new List<Character>();

   
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    /// <summary>
    /// 根據角色名稱查找對應的角色 ID
    /// </summary>
    /// <param name="_name">角色的名稱</param>
    /// <returns>返回角色的 ID，如果未找到則返回 -1</returns>
    public int CharacterNameToID(string _name)
    {
        // 在角色資料庫中查找名稱匹配的角色
        Character character = CharacterDBList.Find((Character _character) => (_character.characterName == _name));

        // 如果角色不存在，回傳 -1 並輸出錯誤信息
        if (character == null)
        {
            Debug.LogError("找不到對應的角色名稱：" + _name);
            return -1;
        }
        else
        {
            // 找到角色則返回角色的 ID
            return character.ID;
        }
    }

    /// <summary>
    /// 根據角色 ID 查找對應的角色名稱
    /// </summary>
    /// <param name="_id">角色的 ID</param>
    /// <returns>返回角色名稱，如果未找到則返回 "找不到名字"</returns>
    public string CharacterIDToName(int _id)
    {
        // 在角色資料庫中查找 ID 匹配的角色
        Character character = CharacterDBList.Find((Character _character) => (_character.ID == _id));

        // 如果角色不存在，返回 "找不到名字" 並輸出錯誤信息
        if (character == null)
        {
            Debug.LogError("找不到對應的角色 ID：" + _id);
            return "找不到名字";
        }
        else
        {
            // 找到角色則返回角色的名稱
            return character.characterName;
        }
    }
}
