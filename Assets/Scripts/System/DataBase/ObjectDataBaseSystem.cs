using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDataBaseSystem : MonoSingleton<ObjectDataBaseSystem>
{
    public List<ObjectData> ObjDBList = new List<ObjectData>();


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObjToInventory(int _id)
    {       
        ObjectData objectData = ObjDBList.Find((ObjectData obj) => (obj.id == _id));
        if (objectData == null)
        {
            Debug.LogError("找不到對應編號");
            return;        
        }
        InventorySystem.instance.objectDataList.Add(objectData);
        InventorySystem.instance.AddObjects(objectData);    
    }   
}
