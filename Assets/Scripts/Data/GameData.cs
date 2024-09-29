using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData//遊戲中所有需要被存檔的資料
{
    //OpponentData
    //ObjectData
    public int currentTime;
    public int currentHeart;
    public int currentDialogueGroupDataID;

    public GameData()
    {
        this.currentTime = 5;   
        this.currentHeart = 5;
        this.currentDialogueGroupDataID = 0;
    }
}
