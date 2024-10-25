using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����Ʈw�t�ΡA�t�d�޲z�����Ʈw��������M��
/// </summary>
public class CharacterDataBaseSystem : MonoSingleton<CharacterDataBaseSystem>
{
    // �����Ʈw�M��A�x�s�Ҧ��������T
    public List<Character> CharacterDBList = new List<Character>();

   
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    /// <summary>
    /// �ھڨ���W�٬d����������� ID
    /// </summary>
    /// <param name="_name">���⪺�W��</param>
    /// <returns>��^���⪺ ID�A�p�G�����h��^ -1</returns>
    public int CharacterNameToID(string _name)
    {
        // �b�����Ʈw���d��W�٤ǰt������
        Character character = CharacterDBList.Find((Character _character) => (_character.characterName == _name));

        // �p�G���⤣�s�b�A�^�� -1 �ÿ�X���~�H��
        if (character == null)
        {
            Debug.LogError("�䤣�����������W�١G" + _name);
            return -1;
        }
        else
        {
            // ��쨤��h��^���⪺ ID
            return character.ID;
        }
    }

    /// <summary>
    /// �ھڨ��� ID �d�����������W��
    /// </summary>
    /// <param name="_id">���⪺ ID</param>
    /// <returns>��^����W�١A�p�G�����h��^ "�䤣��W�r"</returns>
    public string CharacterIDToName(int _id)
    {
        // �b�����Ʈw���d�� ID �ǰt������
        Character character = CharacterDBList.Find((Character _character) => (_character.ID == _id));

        // �p�G���⤣�s�b�A��^ "�䤣��W�r" �ÿ�X���~�H��
        if (character == null)
        {
            Debug.LogError("�䤣����������� ID�G" + _id);
            return "�䤣��W�r";
        }
        else
        {
            // ��쨤��h��^���⪺�W��
            return character.characterName;
        }
    }
}
