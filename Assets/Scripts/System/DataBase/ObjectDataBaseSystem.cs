using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����Ʈw�t�ΡA�޲z�C�����������ƨñN����K�[�쪱�a���I�]��
/// </summary>
public class ObjectDataBaseSystem : MonoSingleton<ObjectDataBaseSystem>
{
    // �����Ʈw�M��A�x�s�Ҧ���������
    public List<ObjectData> ObjDBList = new List<ObjectData>();

 
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    /// <summary>
    /// �N���w ID ������K�[��I�]��
    /// </summary>
    /// <param name="_id">���K�[���� ID</param>
    public void ObjToInventory(int _id)
    {
        // �b�����Ʈw���d�� ID �ǰt��������
        ObjectData objectData = ObjDBList.Find((ObjectData obj) => (obj.id == _id));

        // �p�G���󤣦s�b�A��X���~�H���ðh�X
        if (objectData == null)
        {
            Debug.LogError("�䤣����������� ID�G" + _id);
            return;
        }

        // �Y��쪫��A�N�����ƲK�[��I�]�t��
        InventorySystem.instance.objectDataList.Add(objectData);
        InventorySystem.instance.AddObjects(objectData); // �I�s�I�]�t�Ϊ� AddObjects ��k�ӳB�z���󪺲K�[�޿�
    }
}
