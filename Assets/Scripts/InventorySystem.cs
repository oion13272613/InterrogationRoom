using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;
/// <summary>
/// 物品欄系統
/// </summary>
public class InventorySystem : MonoSingleton<InventorySystem>

{
    [Header("物品列表資料")]//Unity名稱
    public List<ObjectData> objectDataList = new List<ObjectData>();
    /// <summary>
    /// 物件展示組
    /// </summary>
    public List<ObjectDisplay> objectDisplayList = new List<ObjectDisplay>();
    /// <summary>
    /// 物品欄文字
    /// </summary>
    public Text inventoryText;
    public Text descriptionText;
    public GameObject objectPrefab;
    public Transform grid;
    public ObjectData ObjectData;
    public ObjectDisplay selectedObj;//被選取物件    
    public int lastObjIndex = -1;//最後物件序列，開始默認-1

    // Start is called before the first frame update
    void Start()
    {
        CreateObjects();
        //ShowObject();
    }

    void Update()
    {
        SelectControl();
    }

    /// <summary>
    /// 創建物件
    /// </summary>
    private void CreateObjects()
    {      

        //ObjectData objectData = new ObjectData();
        /*
        ObjectData objectData = new ObjectData(1, "牛頭人","吃牛肉");
        ObjectData objectData2 = new ObjectData(2, "雞頭人","吃雞肉");
        ObjectData objectData3 = new ObjectData(3, "狗頭人", "吃狗肉");
        ObjectData objectData4 = new ObjectData(4,"豬頭人", "吃豬肉");

        objectDataList.Add(objectData);
        objectDataList.Add(objectData2);
        objectDataList.Add(objectData3);
        objectDataList.Add(objectData4);
        */
      
        //GameObject obj = Instantiate(objectPrefab, grid);
        //obj.GetComponent<ObjectDisplay>().obj = objectData;
        //obj.GetComponent<ObjectDisplay>().ShowName();

        //生成物件、給予資料、顯示名字
        foreach (ObjectData item in objectDataList)
        {
            lastObjIndex++;
            //生成Prefab物件在grid上，找到ObjectDisplay掛載組件，追蹤到生成的物件
            GameObject obj = Instantiate(objectPrefab, grid);
            //讓objectDisplay記住ObjectDisplay裡的組件
            ObjectDisplay objectDisplay = obj.GetComponent<ObjectDisplay>();
            objectDisplayList.Add(objectDisplay);//為SetSelectedObj作準備
            objectDisplay.obj = item;//將objectDataList裡的資料給予objectDisplay
            objectDisplay.inListIndex = lastObjIndex;
            objectDisplay.ShowName();
        }

        //默認選取第一個物件
        if (lastObjIndex>=0) 
        {
        SetSelectedObj(objectDisplayList[0]);
        }
        
    }

    /// <summary>
    /// 顯示物件
    /// </summary>

    //private void ShowObject()
    //{
    //    ObjectData objectData = new ObjectData();//創建新資料空間
    //    objectData = objectDataList[0];//把數組第一個資料傳入新資料空間
    //    Debug.Log("物件名子是：" + objectData.objectName);

    //    inventoryText.text = objectData.objectName;//將資料空間內的名字賦予UIText組件
    //}

    /// <summary>
    ///物件被選取時調用
    /// </summary>
    public void SetSelectedObj(ObjectDisplay _objectDisplay)
    {  
        if(selectedObj != null)//如果被選取物件不為空
        {
            selectedObj.UnSelect();//被選取物件不顯示選取框
        }             
        selectedObj = _objectDisplay;
        selectedObj.Select();//被選取物件顯示選取框
        ShowSelectedObj();
    }

    /// <summary>
    /// 顯示被選取物件名字
    /// </summary>
    public void ShowSelectedObj()
    {
        if(selectedObj == null) 
        {
        return;
        }       
        ObjectData objectData = selectedObj.obj;
        inventoryText.text = objectData.objectName;
        descriptionText.text = objectData.descriptionText;
    }

    /// <summary>
    /// 選擇控制
    /// </summary>
    public void SelectControl()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))//右方向鍵
        {                        
            Debug.Log("按下右方向鍵");
            int current = selectedObj.inListIndex;//current現在
            current++;
            if (current>lastObjIndex) { return; }

            SetSelectedObj(objectDisplayList[current]);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))//左方向鍵
        {
            Debug.Log("按下左方向鍵");
            int current = selectedObj.inListIndex;//current現在
            current--;
            if (current < 0) { return; }

            SetSelectedObj(objectDisplayList[current]);
        }

    }

}
