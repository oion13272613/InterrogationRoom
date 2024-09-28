using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 顯示物品資訊的類別，負責物品在介面上的顯示和互動。
/// </summary>
public class ObjectDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler//有MonoBehaviour才可以在Unity掛載物件
{
    /// <summary>
    /// 顯示物品名稱的 UI Text 元件。
    /// </summary>
    public Text name;

    /// <summary>
    /// 物品的數據。
    /// </summary>
    public ObjectData obj;

    /// <summary>
    /// 物品在物品列表中的索引位置。
    /// </summary>
    public int inListIndex;

    /// <summary>
    /// 顯示物品被選取時的框框。
    /// </summary>
    public Image measurementFrameSelect;

    /// <summary>
    /// 顯示物品在滑鼠指標上方時的高亮框框。
    /// </summary>
    public Image measurementFrameHighlight;

    /// <summary>
    /// 標記物品是否被選取。
    /// </summary>
    public bool isSelected;

    //public InventorySystem inventorySystem;

    /// <summary>
    /// 在物件初始化時禁用高亮框和選取框。
    /// </summary>
    private void Awake()//Awake 更早載入時運行
    {
       //inventorySystem = GameObject.FindObjectOfType<InventorySystem>();
        OffHighlight();
        UnSelect();
    }

    /// <summary>
    /// 顯示物品的名稱到 UI 元件上。
    /// </summary>
    public void ShowName()
    {
        name.text = obj.objectName;
    }

    /// <summary>
    /// 處理滑鼠指標進入物品區域的事件，顯示高亮框。
    /// </summary>
    /// <param name="eventData">事件數據。</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelected)
        {
            return;// 如果物品已被選取，則不顯示高亮框。
        }
        Highlight();
        //Debug.Log("滑鼠進入");

    }


    /// <summary>
    /// 處理滑鼠指標離開物品區域的事件，隱藏高亮框。
    /// </summary>
    /// <param name="eventData">事件數據。</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        OffHighlight();
    }

    /// <summary>
    /// 處理滑鼠左鍵點擊物品的事件，將物品設為選取狀態。
    /// </summary>
    /// <param name="eventData">事件數據。</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        // 通過 InventorySystem 的單例實例來訪問物品欄系統
        // 設定當前被點擊的物件為選取狀態
        // 'this' 參數指的是當前的 ObjectDisplay 實例，即用戶在 UI 中點擊的物品
        // 這樣可以觸發 InventorySystem 的選取邏輯，更新選取狀態、顯示選取框及詳細信息
        InventorySystem.instance.SetSelectedObj(this);
    }

    /// <summary>
    /// 設置物品為選取狀態，顯示選取框。
    /// </summary> 
    public void Select()
    {
        isSelected = true;
        measurementFrameSelect.enabled = true;
    }

    /// <summary>
    /// 取消物品的選取狀態，隱藏選取框。
    /// </summary>
    public void UnSelect()
    {
        isSelected = false;
        measurementFrameSelect.enabled = false;
    }


    /// <summary>
    /// 顯示物品的高亮框。
    /// </summary>
    public void Highlight()
    {
        measurementFrameHighlight.enabled = true;
    }

    /// <summary>
    /// 隱藏物品的高亮框。
    /// </summary>
    public void OffHighlight()
    {
        measurementFrameHighlight.enabled = false;
    }

}
