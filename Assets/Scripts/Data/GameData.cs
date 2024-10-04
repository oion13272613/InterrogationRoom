using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData//遊戲中所有需要被存檔的資料
{
    //OpponentData
    //ObjectData
    public PlayerTime playerTime;
    public OpponentHeart opponentHeart;
    public int currentDialogueDataID;

    public GameData()
    {
        this.playerTime = new PlayerTime(); 
        this.opponentHeart = new OpponentHeart();
        this.currentDialogueDataID = 0;
    }
}
