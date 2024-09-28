using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataBaseSystem : MonoSingleton<CharacterDataBaseSystem>
{
    public List<Character> CharacterDBList = new List<Character>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public int CharacterNameToID(string _name)
    {
        Character character = CharacterDBList.Find((Character _character) => (_character.characterName == _name));
        if (character == null)
        {
            Debug.LogError("�䤣������s��");
            return -1;
        }
        else
        {
            return character.ID;
        }
    }
    public string CharacterIDToName(int _id)
    {
        Character character = CharacterDBList.Find((Character _character) => (_character.ID == _id));
        if (character == null)
        {
            Debug.LogError("�䤣������s��"+_id);
            return "�䤣��W�r";
        }
        else
        {
            return character.characterName;
        }
    }
}
