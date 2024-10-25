using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEngine.UI.Image;
/// <summary>
/// ���~��t�ΡA�޲z���~����ܻP���
/// </summary>
public class InventorySystem : MonoSingleton<InventorySystem>

{
    [Header("���~�C����")]// �b Unity Inspector ���O����ܪ����D
    public List<ObjectData> objectDataList = new List<ObjectData>();// �x�s�Ҧ����~�ƾڪ��C��

    /// <summary>
    /// ���~�i�ܲե�C��
    /// </summary>
    public List<ObjectDisplay> objectDisplayList = new List<ObjectDisplay>();// �x�s�Ҧ����~��ܲե󪺦C��

    /// <summary>
    /// ���~����ܪ��奻
    /// </summary>
    public Text inventoryText;// ��ܪ��~�W�٪� Text �ե�
    public Text descriptionText;// ��ܪ��~�y�z�� Text �ե�
    public GameObject objectPrefab;// ���~�i�� Prefab�A�Ω�ͦ����~��ܹ�H
    public Transform grid;// ���~�i�ܪ�������
    public ObjectData ObjectData;// �Ω�s�x���~�ƾڪ��ܼ�
    public ObjectDisplay selectedObj;// ��e��������~�i�ܲե�    
    public int lastObjIndex = -1;// �̫�@�Ӫ��~�����ޡA��l�Ƭ� -1  

    private float moveDelay = 0.3f; // ����C�����ʤ������̵u����ɶ��]�H�����^
    private float lastMoveTime = 0f; // �O���W�����ʪ��ɶ��A�q�{�� 0�]�C���}�l�ɶ��^
    public ScrollRect scrollRect; // Scroll Rect �ե�A�Ω󱱨�~�檺�u��
    private bool isDragging = false; // �˴��Ʊ�O�_���b�Q�ާ@

    void Start()
    {
        CreateObjects();
        //ShowObject(); ��ܪ��~���W��
    }

    void Update()
    {
        SelectControl();// �B�z���~����޿�
        CheckScrolling();// �B�z���~��u���޿�
    }

    /// <summary>
    /// �Ыب���ܪ��~
    /// </summary>
    public void CreateObjects()
    {
        if (objectDataList==null) 
        {
            return;
        }
        // �M�����~�ƾڦC��A���C�Ӫ��~�ͦ���������ܹ�H
        foreach (ObjectData item in objectDataList)
        {
            AddObjects(item);       
        }

        //�q�{����C�����Ĥ@�Ӫ��~
        if (lastObjIndex>=0) 
        {
        SetSelectedObj(objectDisplayList[0]);// �]�m�Ĥ@�Ӫ��~��������A
        }
        
    }

    public void AddObjects(ObjectData oD)
    {
        //if (CheckID(oD.id)) 
        //{
        //return ;        
        //}
        //objectDataList.Add(oD);
        lastObjIndex++;// ��s���~������                     
        GameObject obj = Instantiate(objectPrefab, grid);//�ͦ����~ Prefab �ó]�m������ grid�A���ObjectDisplay�����ե�A�l�ܨ�ͦ�������
        ObjectDisplay objectDisplay = obj.GetComponent<ObjectDisplay>();
        objectDisplayList.Add(objectDisplay);//�N���~�i�ܲե�K�[��C��
        objectDisplay.obj = oD;//�]�m���~�ƾڡA�NobjectDataList�̪���Ƶ���objectDisplay
        objectDisplay.inListIndex = lastObjIndex;//�]�m���~������
        objectDisplay.ShowName();//��ܪ��~�W��
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
    /// �]�m��������~�ç�s���
    /// </summary>
    /// <param name="_objectDisplay">�n��������~�i�ܲե�</param>
    public void SetSelectedObj(ObjectDisplay _objectDisplay)
    {  
        if(selectedObj != null)//�p�G�Q������󤣬���(�p�G�w����������~)
        {
            selectedObj.UnSelect();//����������A
        }             
        selectedObj = _objectDisplay;//��s��������~
        selectedObj.Select();//��ܿ����
        ShowSelectedObj();//��s��ܿ�����~���W�٩M�y�z
        //CenterSelectedObject(); //�T�O������~�b���f��
    }

    /// <summary>
    /// ��ܿ�����~���W�٩M�y�z
    /// </summary>
    public void ShowSelectedObj()
    {
        if(selectedObj == null) //�p�G�S��������~
        {
        return;
        }       
        ObjectData objectData = selectedObj.obj;//���������~���ƾ�
        inventoryText.text = objectData.objectName;//��s���~�W�����
        descriptionText.text = objectData.descriptionText;//��s���~�y�z���
    }

    /// <summary>
    /// �B�z���~����������޿�
    /// </summary>
    public void SelectControl()
    {     
        // �ˬd�O�_�w�g�L�F���w�����ʩ���ɶ�
        if (Time.time - lastMoveTime > moveDelay)
        {
            // �p�G�k��V��Q�������
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("��������k��V��");
                int current = selectedObj.inListIndex; //�����e������~������
                current++; //�N���޼W�[1�A���V�U�@�Ӫ��~

                // �p�G���޶W�L�F���~�C���̫�@�Ӫ��~�A�h�^��Ĥ@�Ӫ��~
                if (current > lastObjIndex)
                {
                    current = 0; // �N���ޭ��m���Ĥ@�Ӫ��~
                }

                // �]�m�s��������~
                SetSelectedObj(objectDisplayList[current]);
                CenterSelectedObject();
                // ��s�W�����ʪ��ɶ�����e�ɶ�
                lastMoveTime = Time.time;
            }

            // �p�G����V��Q�������
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("�����������V��");
                int current = selectedObj.inListIndex; //�����e������~������
                current--; //�N���޴��1�A���V�W�@�Ӫ��~

                // �p�G���ޤp��0�A�h�^��̫�@�Ӫ��~
                if (current < 0)
                {
                    current = lastObjIndex; //�N���޳]�m���̫�@�Ӫ��~
                }

                //�]�m�s��������~
                SetSelectedObj(objectDisplayList[current]);
                CenterSelectedObject();
                //��s�W�����ʪ��ɶ�����e�ɶ�
                lastMoveTime = Time.time;
            }
        }
    }

    /// <summary>
    /// �ˬd�óB�z���~�檺�u��
    /// </summary>
    private void CheckScrolling()
    {
        // �ˬd�O�_���b��ʷƱ�]�����^
        //horizontalScrollbar �O ScrollRect ���@���ݩʡA��ܤ����u�ʱ��� Scrollbar �ե�C
        if (scrollRect.horizontalScrollbar != null && scrollRect.horizontalScrollbar.IsActive())
        {//IsActive()�GGameObject���@�Ӥ�k�A�Ω��ˬd����O�_�ҥΡC���|��^�@�ӥ��L�ȡA���ܸӪ���O�_�ҥΡC
            isDragging = true; // �Ʊ쥿�b�Q�ާ@
        }
        else
        {
            isDragging = false; // �Ʊ쥼�Q�ާ@
        }

        // �p�G�Ʊ쥿�b�Q��ʡA�h���i��۰ʺu��
        if (isDragging)
        {
            return;
        }

        // ����~�檺�u�ʡ]�����^
        if (Time.time - lastMoveTime > moveDelay)
        {
            Vector2 scrollDelta = Vector2.zero;//scrollDelta�u���ܶq�AVector2.zero��l��(0,0)

            if (Input.GetKey(KeyCode.RightArrow))
            {
                scrollDelta.x += 1; // ���U�k��V��A�V�k����
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                scrollDelta.x -= 1; // ���U����V��A�V������
            }

            if (scrollDelta != Vector2.zero)
            {
                // �ھںu�ʶq�ӽվ� Scroll Rect ��������m
                //horizontalNormalizedPosition: �o�O ScrollRect �ե󪺤@���ݩʡA����ܤ����u�ʱ�����e��m�C�o�ӭȽd��q 0 �� 1
                scrollRect.horizontalNormalizedPosition += scrollDelta.x * 0.1f; // �վ㲾�ʳt�סA0.1 �O���ʪ��t�v

                //Mathf.Clamp01 �O Unity ���ƾǨ�ơA�Ψӭ���ƭȦb 0 �M 1 �����C�����@�άO�G
               // Mathf.Clamp01(x): �p�G x �b 0 �M 1 �����A�h��^ x�C�p�G x �p�� 0�A�h��^ 0�F�p�G x �j�� 1�A�h��^ 1�C
                scrollRect.horizontalNormalizedPosition = Mathf.Clamp01(scrollRect.horizontalNormalizedPosition); // �O�I�A����u�ʽd��b 0 �M 1 ����
                lastMoveTime = Time.time; // ��s���ʮɶ�
            }
        }
    }

    /// <summary>
    /// �T�O������~�b���f���~�����
    /// </summary>
    private void CenterSelectedObject()
    {
        if (scrollRect != null && selectedObj != null)
        {
            
            Debug.Log("�~��");
            RectTransform itemRectTransform = selectedObj.GetComponent<RectTransform>(); // ���������~�� RectTransform
            //RectTransform�G�Ω�B�z UI �����]�p���s�B�奻�B�Ϲ����^�����M��m���ե�C

            //viewport���f
            RectTransform viewport = scrollRect.viewport; // ��� Scroll Rect �����f

            // ������~�M���f���|�Ө�����m
            Vector3[] itemCorners = new Vector3[4];//itemCorners���~4��
            //GetWorldCorners(Vector3[] corners) �O RectTransform ���@�Ӥ�k�A�Ω��R corners �}�C�A�Ӱ}�C�]�t�F RectTransform �b�@�ɪŶ������|�Ө�����m�C
            itemRectTransform.GetWorldCorners(itemCorners);
            Vector3[] viewportCorners = new Vector3[4];
            viewport.GetWorldCorners(viewportCorners);

            // �p�⪫�~�� x �����I
            float itemCenterX = (itemCorners[0].x + itemCorners[2].x) / 2f;
            Debug.Log(itemCenterX);
            // �p����f�� x �����I
            float viewportCenterX = (viewportCorners[0].x + viewportCorners[2].x) / 2f;
            
            // �p�⪫�~�۹����f�������q
            //rect�ݩʬO�@�� Rect ���c�A�䤤�]�t�F RectTransform ���e�סB���סB��m���H���C
            float itemWidth = itemRectTransform.rect.width;//������~�e��
            float viewportWidth = viewport.rect.width;//������f�e��


            // �p�⪫�~�����I�b���f�����۹��m //itemRelativePosition���~�۹��m
            float itemRelativePosition = (itemCenterX - viewportCenterX) / (viewportWidth - itemWidth);
           
            // �վ� Scroll Rect ��������m
            scrollRect.horizontalNormalizedPosition += itemRelativePosition/2f;//�����h�֥[�h��
            //Debug.Log(itemRelativePosition);
            scrollRect.horizontalNormalizedPosition = Mathf.Clamp01(scrollRect.horizontalNormalizedPosition); // �T�O�b 0 �M 1 ����
            // �w�� UI �i�歫�إH�T�O��s
            Canvas.ForceUpdateCanvases();        
        }
    }
    // �����ܪ�����
    public void SubmitSelectedObj()
    {
        // �ե� MainSystem ��Ҥ��� SubmitObjectID ��k�A�N�襤������ ID �����D�t��
        MainSystem.instance.SubmitObjectID(selectedObj.obj.id);
    }

    private bool CheckID(int _id)
    {
        return (objectDataList.Find((ObjectData obj) => (obj.id == _id)));
    }
}
