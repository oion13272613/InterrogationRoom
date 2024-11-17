using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理對手的血量，包括最大血量和當前血量。提供設置、減少和重置血量的功能。
/// </summary>
[Serializable]
public class OpponentHeart
{

    [SerializeField] private int maxHeart;// 最大血量：表示對手可以擁有的最大血量    
    [SerializeField] private int currentHeart;// 當前血量：表示對手當前擁有的血量

    /// <summary>
    /// 最大血量的公開只讀屬性
    /// </summary>
    public int MaxHeart
    {
        get { return maxHeart; }
    }

    /// <summary>
    /// 當前血量的公開只讀屬性
    /// </summary>
    public int CurrentHeart
    {
        get { return currentHeart; }
        set { currentHeart = value;}
    }

    /// <summary>
    /// 設置血量，初始化最大血量並將當前血量設置為最大血量
    /// </summary>
    /// <param name="max">初始化的最大血量值</param>
    public void SetHeart(int max)
    {
        maxHeart = max;       // 設置最大血量為傳入的值
        currentHeart = maxHeart; // 將當前血量設置為最大血量
    }

    /// <summary>
    /// 減少當前血量
    /// </summary>
    /// <param name="value">要減少的血量量</param>
    public void ReduceHeart(int value)
    {
        currentHeart -= value; // 減少當前血量

        // 如果當前血量小於或等於0，則將其設置為0
        if (currentHeart <= 0)
        {
            currentHeart = 0;
        }
    }

    /// <summary>
    /// 重置當前血量為最大血量
    /// </summary>
    public void ResetHeart()
    {
        currentHeart = maxHeart; // 將當前血量設置為最大血量
    }
}
