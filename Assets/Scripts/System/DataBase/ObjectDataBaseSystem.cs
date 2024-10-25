using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物件資料庫系統，管理遊戲中的物件資料並將物件添加到玩家的背包中
/// </summary>
public class ObjectDataBaseSystem : MonoSingleton<ObjectDataBaseSystem>
{
    // 物件資料庫清單，儲存所有的物件資料
    public List<ObjectData> ObjDBList = new List<ObjectData>();

 
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    /// <summary>
    /// 將指定 ID 的物件添加到背包中
    /// </summary>
    /// <param name="_id">欲添加物件的 ID</param>
    public void ObjToInventory(int _id)
    {
        // 在物件資料庫中查找 ID 匹配的物件資料
        ObjectData objectData = ObjDBList.Find((ObjectData obj) => (obj.id == _id));

        // 如果物件不存在，輸出錯誤信息並退出
        if (objectData == null)
        {
            Debug.LogError("找不到對應的物件 ID：" + _id);
            return;
        }

        // 若找到物件，將物件資料添加到背包系統
        InventorySystem.instance.objectDataList.Add(objectData);
        InventorySystem.instance.AddObjects(objectData); // 呼叫背包系統的 AddObjects 方法來處理物件的添加邏輯
    }
}
