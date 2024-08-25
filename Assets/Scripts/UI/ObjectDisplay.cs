using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 物件展示
/// </summary>
public class ObjectDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler//有MonoBehaviour才可以在Unity掛載物件
{
    public Text name;
    public ObjectData obj;
    public int inListIndex;//物品列表的索引

    public Image measurementFrameHighlight;//亮框
    public Image measurementFrameSelect;//被選取框
    public bool isSelected;
    //public InventorySystem inventorySystem;

    private void Awake()//Awake 更早載入時運行
    {
       //inventorySystem = GameObject.FindObjectOfType<InventorySystem>();
        OffHighlight();
        UnSelect();
    }

    /// <summary>
    /// 顯示物件名字
    /// </summary>
    public void ShowName()
    {
        name.text = obj.objectName;
    }

    //滑鼠指標擺放到物件上，顯示亮框
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelected)
        {
            return;
        }
        Highlight();
        //Debug.Log("滑鼠進入");

    }
   

    //滑鼠指標離開物件後，不顯示亮框
    public void OnPointerExit(PointerEventData eventData)
    {
        OffHighlight();
    }

    //滑鼠左鍵點擊物件，顯示被選取框
    public void OnPointerDown(PointerEventData eventData)
    {
        //inventorySystem.SetSelectedObj(this);
        InventorySystem.instance.SetSelectedObj(this);
    }

    //滑鼠左鍵點擊別的物件，不顯示被選取框  
    public void Select()
    {
        isSelected = true;
        measurementFrameSelect.enabled = true;
    }

    //物件顯示被選取框時，不顯示亮框
    public void UnSelect()
    {
        isSelected = false;
        measurementFrameSelect.enabled = false;
    }


    public void Highlight()
    {
        measurementFrameHighlight.enabled = true;
    }

    public void OffHighlight()
    {
        measurementFrameHighlight.enabled = false;
    }

}
