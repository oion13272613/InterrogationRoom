using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData: Character
{        
    public List<ObjectData> objectDataList;//���~��

    public PlayerData(int _id, string _playerName):base(_id,_playerName)
    {
        objectDataList = new List<ObjectData>();
    }
}
