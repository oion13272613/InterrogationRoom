using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;
/// <summary>
/// ���~��t��
/// </summary>
public class InventorySystem : MonoSingleton<InventorySystem>

{
    [Header("���~�C����")]//Unity�W��
    public List<ObjectData> objectDataList = new List<ObjectData>();
    /// <summary>
    /// ����i�ܲ�
    /// </summary>
    public List<ObjectDisplay> objectDisplayList = new List<ObjectDisplay>();
    /// <summary>
    /// ���~���r
    /// </summary>
    public Text inventoryText;
    public Text descriptionText;
    public GameObject objectPrefab;
    public Transform grid;
    public ObjectData ObjectData;
    public ObjectDisplay selectedObj;//�Q�������    
    public int lastObjIndex = -1;//�̫᪫��ǦC�A�}�l�q�{-1

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
    /// �Ыت���
    /// </summary>
    private void CreateObjects()
    {      

        //ObjectData objectData = new ObjectData();
        /*
        ObjectData objectData = new ObjectData(1, "���Y�H","�Y����");
        ObjectData objectData2 = new ObjectData(2, "���Y�H","�Y����");
        ObjectData objectData3 = new ObjectData(3, "���Y�H", "�Y����");
        ObjectData objectData4 = new ObjectData(4,"���Y�H", "�Y�ަ�");

        objectDataList.Add(objectData);
        objectDataList.Add(objectData2);
        objectDataList.Add(objectData3);
        objectDataList.Add(objectData4);
        */
      
        //GameObject obj = Instantiate(objectPrefab, grid);
        //obj.GetComponent<ObjectDisplay>().obj = objectData;
        //obj.GetComponent<ObjectDisplay>().ShowName();

        //�ͦ�����B������ơB��ܦW�r
        foreach (ObjectData item in objectDataList)
        {
            lastObjIndex++;
            //�ͦ�Prefab����bgrid�W�A���ObjectDisplay�����ե�A�l�ܨ�ͦ�������
            GameObject obj = Instantiate(objectPrefab, grid);
            //��objectDisplay�O��ObjectDisplay�̪��ե�
            ObjectDisplay objectDisplay = obj.GetComponent<ObjectDisplay>();
            objectDisplayList.Add(objectDisplay);//��SetSelectedObj�@�ǳ�
            objectDisplay.obj = item;//�NobjectDataList�̪���Ƶ���objectDisplay
            objectDisplay.inListIndex = lastObjIndex;
            objectDisplay.ShowName();
        }

        //�q�{����Ĥ@�Ӫ���
        if (lastObjIndex>=0) 
        {
        SetSelectedObj(objectDisplayList[0]);
        }
        
    }

    /// <summary>
    /// ��ܪ���
    /// </summary>

    //private void ShowObject()
    //{
    //    ObjectData objectData = new ObjectData();//�Ыطs��ƪŶ�
    //    objectData = objectDataList[0];//��ƲղĤ@�Ӹ�ƶǤJ�s��ƪŶ�
    //    Debug.Log("����W�l�O�G" + objectData.objectName);

    //    inventoryText.text = objectData.objectName;//�N��ƪŶ������W�r�ᤩUIText�ե�
    //}

    /// <summary>
    ///����Q����ɽե�
    /// </summary>
    public void SetSelectedObj(ObjectDisplay _objectDisplay)
    {  
        if(selectedObj != null)//�p�G�Q������󤣬���
        {
            selectedObj.UnSelect();//�Q���������ܿ����
        }             
        selectedObj = _objectDisplay;
        selectedObj.Select();//�Q���������ܿ����
        ShowSelectedObj();
    }

    /// <summary>
    /// ��ܳQ�������W�r
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
    /// ��ܱ���
    /// </summary>
    public void SelectControl()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))//�k��V��
        {                        
            Debug.Log("���U�k��V��");
            int current = selectedObj.inListIndex;//current�{�b
            current++;
            if (current>lastObjIndex) { return; }

            SetSelectedObj(objectDisplayList[current]);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))//����V��
        {
            Debug.Log("���U����V��");
            int current = selectedObj.inListIndex;//current�{�b
            current--;
            if (current < 0) { return; }

            SetSelectedObj(objectDisplayList[current]);
        }

    }

}
