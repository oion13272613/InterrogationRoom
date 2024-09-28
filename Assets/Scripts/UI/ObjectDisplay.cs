using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ��ܪ��~��T�����O�A�t�d���~�b�����W����ܩM���ʡC
/// </summary>
public class ObjectDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler//��MonoBehaviour�~�i�H�bUnity��������
{
    /// <summary>
    /// ��ܪ��~�W�٪� UI Text ����C
    /// </summary>
    public Text name;

    /// <summary>
    /// ���~���ƾڡC
    /// </summary>
    public ObjectData obj;

    /// <summary>
    /// ���~�b���~�C�������ަ�m�C
    /// </summary>
    public int inListIndex;

    /// <summary>
    /// ��ܪ��~�Q����ɪ��خءC
    /// </summary>
    public Image measurementFrameSelect;

    /// <summary>
    /// ��ܪ��~�b�ƹ����ФW��ɪ����G�خءC
    /// </summary>
    public Image measurementFrameHighlight;

    /// <summary>
    /// �аO���~�O�_�Q����C
    /// </summary>
    public bool isSelected;

    //public InventorySystem inventorySystem;

    /// <summary>
    /// �b�����l�ƮɸT�ΰ��G�ةM����ءC
    /// </summary>
    private void Awake()//Awake �󦭸��J�ɹB��
    {
       //inventorySystem = GameObject.FindObjectOfType<InventorySystem>();
        OffHighlight();
        UnSelect();
    }

    /// <summary>
    /// ��ܪ��~���W�٨� UI ����W�C
    /// </summary>
    public void ShowName()
    {
        name.text = obj.objectName;
    }

    /// <summary>
    /// �B�z�ƹ����жi�J���~�ϰ쪺�ƥ�A��ܰ��G�ءC
    /// </summary>
    /// <param name="eventData">�ƥ�ƾڡC</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelected)
        {
            return;// �p�G���~�w�Q����A�h����ܰ��G�ءC
        }
        Highlight();
        //Debug.Log("�ƹ��i�J");

    }


    /// <summary>
    /// �B�z�ƹ��������}���~�ϰ쪺�ƥ�A���ð��G�ءC
    /// </summary>
    /// <param name="eventData">�ƥ�ƾڡC</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        OffHighlight();
    }

    /// <summary>
    /// �B�z�ƹ������I�����~���ƥ�A�N���~�]��������A�C
    /// </summary>
    /// <param name="eventData">�ƥ�ƾڡC</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        // �q�L InventorySystem ����ҹ�ҨӳX�ݪ��~��t��
        // �]�w��e�Q�I�������󬰿�����A
        // 'this' �Ѽƫ����O��e�� ObjectDisplay ��ҡA�Y�Τ�b UI ���I�������~
        // �o�˥i�HĲ�o InventorySystem ������޿�A��s������A�B��ܿ���ؤθԲӫH��
        InventorySystem.instance.SetSelectedObj(this);
    }

    /// <summary>
    /// �]�m���~��������A�A��ܿ���ءC
    /// </summary> 
    public void Select()
    {
        isSelected = true;
        measurementFrameSelect.enabled = true;
    }

    /// <summary>
    /// �������~��������A�A���ÿ���ءC
    /// </summary>
    public void UnSelect()
    {
        isSelected = false;
        measurementFrameSelect.enabled = false;
    }


    /// <summary>
    /// ��ܪ��~�����G�ءC
    /// </summary>
    public void Highlight()
    {
        measurementFrameHighlight.enabled = true;
    }

    /// <summary>
    /// ���ê��~�����G�ءC
    /// </summary>
    public void OffHighlight()
    {
        measurementFrameHighlight.enabled = false;
    }

}
