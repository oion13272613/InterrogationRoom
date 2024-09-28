using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // 標記這個類別可以被序列化，以便於保存和編輯
[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Create SO/CharacterData")]
/// <summary>
/// OpponentData擴展 Character 類別，表示對手的數據，包括對手的血量。
/// </summary>
public class OpponentData : Character
{   
    public OpponentHeart opponentHeart;// 對手的血量管理

    /// <summary>
    /// 基本構造函數，初始化對手的 ID 和名稱，並設置對手的血量為默認值 5。
    /// </summary>
    /// <param name="_id">對手的唯一識別碼</param>
    /// <param name="opponentName">對手的名稱</param>
    public OpponentData(int _id, string opponentName) : base(_id, opponentName)
    {
        // 初始化 opponentHeart 為新的 OpponentHeart 實例
        opponentHeart = new OpponentHeart();
        // 設置血量為默認值 5
        opponentHeart.SetHeart(5);
    }

    /// <summary>
    /// 重載構造函數，初始化對手的 ID 和名稱，並設置對手的血量為指定的值。
    /// </summary>
    /// <param name="_id">對手的唯一識別碼</param>
    /// <param name="opponentName">對手的名稱</param>
    /// <param name="_maxHeart">對手的最大血量</param>
    public OpponentData(int _id, string opponentName, int _maxHeart) : base(_id, opponentName)
    {
        // 初始化 opponentHeart 為新的 OpponentHeart 實例
        opponentHeart = new OpponentHeart();
        // 設置血量為指定的最大血量
        opponentHeart.SetHeart(_maxHeart);
    }
}
