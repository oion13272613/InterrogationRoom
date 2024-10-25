using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEngine.UI.Image;
/// <summary>
/// 物品欄系統，管理物品的顯示與選擇
/// </summary>
public class InventorySystem : MonoSingleton<InventorySystem>

{
    [Header("物品列表資料")]// 在 Unity Inspector 面板中顯示的標題
    public List<ObjectData> objectDataList = new List<ObjectData>();// 儲存所有物品數據的列表

    /// <summary>
    /// 物品展示組件列表
    /// </summary>
    public List<ObjectDisplay> objectDisplayList = new List<ObjectDisplay>();// 儲存所有物品顯示組件的列表

    /// <summary>
    /// 物品欄顯示的文本
    /// </summary>
    public Text inventoryText;// 顯示物品名稱的 Text 組件
    public Text descriptionText;// 顯示物品描述的 Text 組件
    public GameObject objectPrefab;// 物品展示 Prefab，用於生成物品顯示對象
    public Transform grid;// 物品展示的父物件
    public ObjectData ObjectData;// 用於存儲物品數據的變數
    public ObjectDisplay selectedObj;// 當前選取的物品展示組件    
    public int lastObjIndex = -1;// 最後一個物品的索引，初始化為 -1  

    private float moveDelay = 0.3f; // 控制每次移動之間的最短延遲時間（以秒為單位）
    private float lastMoveTime = 0f; // 記錄上次移動的時間，默認為 0（遊戲開始時間）
    public ScrollRect scrollRect; // Scroll Rect 組件，用於控制物品欄的滾動
    private bool isDragging = false; // 檢測滑桿是否正在被操作

    void Start()
    {
        CreateObjects();
        //ShowObject(); 顯示物品的名稱
    }

    void Update()
    {
        SelectControl();// 處理物品選取邏輯
        CheckScrolling();// 處理物品欄滾動邏輯
    }

    /// <summary>
    /// 創建並顯示物品
    /// </summary>
    public void CreateObjects()
    {
        if (objectDataList==null) 
        {
            return;
        }
        // 遍歷物品數據列表，為每個物品生成對應的顯示對象
        foreach (ObjectData item in objectDataList)
        {
            AddObjects(item);       
        }

        //默認選取列表中的第一個物品
        if (lastObjIndex>=0) 
        {
        SetSelectedObj(objectDisplayList[0]);// 設置第一個物品為選取狀態
        }
        
    }

    public void AddObjects(ObjectData oD)
    {
        //if (CheckID(oD.id)) 
        //{
        //return ;        
        //}
        //objectDataList.Add(oD);
        lastObjIndex++;// 更新物品的索引                     
        GameObject obj = Instantiate(objectPrefab, grid);//生成物品 Prefab 並設置父物件為 grid，找到ObjectDisplay掛載組件，追蹤到生成的物件
        ObjectDisplay objectDisplay = obj.GetComponent<ObjectDisplay>();
        objectDisplayList.Add(objectDisplay);//將物品展示組件添加到列表中
        objectDisplay.obj = oD;//設置物品數據，將objectDataList裡的資料給予objectDisplay
        objectDisplay.inListIndex = lastObjIndex;//設置物品的索引
        objectDisplay.ShowName();//顯示物品名稱
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
    /// 設置選取的物品並更新顯示
    /// </summary>
    /// <param name="_objectDisplay">要選取的物品展示組件</param>
    public void SetSelectedObj(ObjectDisplay _objectDisplay)
    {  
        if(selectedObj != null)//如果被選取物件不為空(如果已有選取的物品)
        {
            selectedObj.UnSelect();//取消選取狀態
        }             
        selectedObj = _objectDisplay;//更新選取的物品
        selectedObj.Select();//顯示選取框
        ShowSelectedObj();//更新顯示選取物品的名稱和描述
        //CenterSelectedObject(); //確保選取物品在視口中
    }

    /// <summary>
    /// 顯示選取物品的名稱和描述
    /// </summary>
    public void ShowSelectedObj()
    {
        if(selectedObj == null) //如果沒有選取物品
        {
        return;
        }       
        ObjectData objectData = selectedObj.obj;//獲取選取物品的數據
        inventoryText.text = objectData.objectName;//更新物品名稱顯示
        descriptionText.text = objectData.descriptionText;//更新物品描述顯示
    }

    /// <summary>
    /// 處理物品選取的控制邏輯
    /// </summary>
    public void SelectControl()
    {     
        // 檢查是否已經過了指定的移動延遲時間
        if (Time.time - lastMoveTime > moveDelay)
        {
            // 如果右方向鍵被持續按住
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("持續按壓右方向鍵");
                int current = selectedObj.inListIndex; //獲取當前選取物品的索引
                current++; //將索引增加1，指向下一個物品

                // 如果索引超過了物品列表的最後一個物品，則回到第一個物品
                if (current > lastObjIndex)
                {
                    current = 0; // 將索引重置為第一個物品
                }

                // 設置新的選取物品
                SetSelectedObj(objectDisplayList[current]);
                CenterSelectedObject();
                // 更新上次移動的時間為當前時間
                lastMoveTime = Time.time;
            }

            // 如果左方向鍵被持續按住
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("持續按壓左方向鍵");
                int current = selectedObj.inListIndex; //獲取當前選取物品的索引
                current--; //將索引減少1，指向上一個物品

                // 如果索引小於0，則回到最後一個物品
                if (current < 0)
                {
                    current = lastObjIndex; //將索引設置為最後一個物品
                }

                //設置新的選取物品
                SetSelectedObj(objectDisplayList[current]);
                CenterSelectedObject();
                //更新上次移動的時間為當前時間
                lastMoveTime = Time.time;
            }
        }
    }

    /// <summary>
    /// 檢查並處理物品欄的滾動
    /// </summary>
    private void CheckScrolling()
    {
        // 檢查是否正在拖動滑桿（水平）
        //horizontalScrollbar 是 ScrollRect 的一個屬性，表示水平滾動條的 Scrollbar 組件。
        if (scrollRect.horizontalScrollbar != null && scrollRect.horizontalScrollbar.IsActive())
        {//IsActive()：GameObject的一個方法，用於檢查物件是否啟用。它會返回一個布林值，指示該物件是否啟用。
            isDragging = true; // 滑桿正在被操作
        }
        else
        {
            isDragging = false; // 滑桿未被操作
        }

        // 如果滑桿正在被拖動，則不進行自動滾動
        if (isDragging)
        {
            return;
        }

        // 控制物品欄的滾動（水平）
        if (Time.time - lastMoveTime > moveDelay)
        {
            Vector2 scrollDelta = Vector2.zero;//scrollDelta滾動變量，Vector2.zero初始化(0,0)

            if (Input.GetKey(KeyCode.RightArrow))
            {
                scrollDelta.x += 1; // 按下右方向鍵，向右移動
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                scrollDelta.x -= 1; // 按下左方向鍵，向左移動
            }

            if (scrollDelta != Vector2.zero)
            {
                // 根據滾動量來調整 Scroll Rect 的水平位置
                //horizontalNormalizedPosition: 這是 ScrollRect 組件的一個屬性，它表示水平滾動條的當前位置。這個值範圍從 0 到 1
                scrollRect.horizontalNormalizedPosition += scrollDelta.x * 0.1f; // 調整移動速度，0.1 是移動的速率

                //Mathf.Clamp01 是 Unity 的數學函數，用來限制數值在 0 和 1 之間。它的作用是：
               // Mathf.Clamp01(x): 如果 x 在 0 和 1 之間，則返回 x。如果 x 小於 0，則返回 0；如果 x 大於 1，則返回 1。
                scrollRect.horizontalNormalizedPosition = Mathf.Clamp01(scrollRect.horizontalNormalizedPosition); // 保險，限制滾動範圍在 0 和 1 之間
                lastMoveTime = Time.time; // 更新移動時間
            }
        }
    }

    /// <summary>
    /// 確保選取物品在視口中居中顯示
    /// </summary>
    private void CenterSelectedObject()
    {
        if (scrollRect != null && selectedObj != null)
        {
            
            Debug.Log("居中");
            RectTransform itemRectTransform = selectedObj.GetComponent<RectTransform>(); // 獲取選取物品的 RectTransform
            //RectTransform：用於處理 UI 元素（如按鈕、文本、圖像等）布局和位置的組件。

            //viewport視口
            RectTransform viewport = scrollRect.viewport; // 獲取 Scroll Rect 的視口

            // 獲取物品和視口的四個角的位置
            Vector3[] itemCorners = new Vector3[4];//itemCorners物品4角
            //GetWorldCorners(Vector3[] corners) 是 RectTransform 的一個方法，用於填充 corners 陣列，該陣列包含了 RectTransform 在世界空間中的四個角的位置。
            itemRectTransform.GetWorldCorners(itemCorners);
            Vector3[] viewportCorners = new Vector3[4];
            viewport.GetWorldCorners(viewportCorners);

            // 計算物品的 x 中心點
            float itemCenterX = (itemCorners[0].x + itemCorners[2].x) / 2f;
            Debug.Log(itemCenterX);
            // 計算視口的 x 中心點
            float viewportCenterX = (viewportCorners[0].x + viewportCorners[2].x) / 2f;
            
            // 計算物品相對於視口的偏移量
            //rect屬性是一個 Rect 結構，其中包含了 RectTransform 的寬度、高度、位置等信息。
            float itemWidth = itemRectTransform.rect.width;//獲取物品寬度
            float viewportWidth = viewport.rect.width;//獲取視口寬度


            // 計算物品中心點在視口中的相對位置 //itemRelativePosition物品相對位置
            float itemRelativePosition = (itemCenterX - viewportCenterX) / (viewportWidth - itemWidth);
           
            // 調整 Scroll Rect 的水平位置
            scrollRect.horizontalNormalizedPosition += itemRelativePosition/2f;//偏移多少加多少
            //Debug.Log(itemRelativePosition);
            scrollRect.horizontalNormalizedPosition = Mathf.Clamp01(scrollRect.horizontalNormalizedPosition); // 確保在 0 和 1 之間
            // 針對 UI 進行重建以確保更新
            Canvas.ForceUpdateCanvases();        
        }
    }
    // 提交選擇的物件
    public void SubmitSelectedObj()
    {
        // 調用 MainSystem 實例中的 SubmitObjectID 方法，將選中的物件 ID 提交到主系統
        MainSystem.instance.SubmitObjectID(selectedObj.obj.id);
    }

    private bool CheckID(int _id)
    {
        return (objectDataList.Find((ObjectData obj) => (obj.id == _id)));
    }
}
