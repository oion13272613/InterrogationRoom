using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ����i��
/// </summary>
public class ObjectDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler//��MonoBehaviour�~�i�H�bUnity��������
{
    public Text name;
    public ObjectData obj;
    public int inListIndex;//���~�C������

    public Image measurementFrameHighlight;//�G��
    public Image measurementFrameSelect;//�Q�����
    public bool isSelected;
    //public InventorySystem inventorySystem;

    private void Awake()//Awake �󦭸��J�ɹB��
    {
       //inventorySystem = GameObject.FindObjectOfType<InventorySystem>();
        OffHighlight();
        UnSelect();
    }

    /// <summary>
    /// ��ܪ���W�r
    /// </summary>
    public void ShowName()
    {
        name.text = obj.objectName;
    }

    //�ƹ������\��쪫��W�A��ܫG��
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelected)
        {
            return;
        }
        Highlight();
        //Debug.Log("�ƹ��i�J");

    }
   

    //�ƹ��������}�����A����ܫG��
    public void OnPointerExit(PointerEventData eventData)
    {
        OffHighlight();
    }

    //�ƹ������I������A��ܳQ�����
    public void OnPointerDown(PointerEventData eventData)
    {
        //inventorySystem.SetSelectedObj(this);
        InventorySystem.instance.SetSelectedObj(this);
    }

    //�ƹ������I���O������A����ܳQ�����  
    public void Select()
    {
        isSelected = true;
        measurementFrameSelect.enabled = true;
    }

    //������ܳQ����خɡA����ܫG��
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
