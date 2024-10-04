using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData//�C�����Ҧ��ݭn�Q�s�ɪ����
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
