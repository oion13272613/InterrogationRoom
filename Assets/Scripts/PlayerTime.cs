using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理玩家的時間狀態，包括最大時間和當前時間。提供設置、減少和重置時間的功能。
/// </summary>
[Serializable]
public class PlayerTime
{
    [SerializeField] private int maxTime;// 最大時間：表示玩家可以擁有的最大時間量   
    [SerializeField] private int currentTime;// 當前時間：表示玩家當前擁有的時間量

    /// <summary>
    /// 最大時間的公開只讀屬性
    /// </summary>
    public int MaxTime
    {
        get { return maxTime; }
    }

    /// <summary>
    /// 當前時間的公開只讀屬性
    /// </summary>
    public int CurrentTime
    {
        get { return currentTime; }
        set { currentTime = value;}
    }
   
    /// <summary>
    /// 設置時間，初始化最大時間並將當前時間設置為最大時間
    /// </summary>
    /// <param name="max">初始化的最大時間值</param>
    public void SetTime(int max)
    {
        maxTime = max;       // 設置最大時間為傳入的值
        currentTime = maxTime; // 將當前時間設置為最大時間
    }

    /// <summary>
    /// 減少當前時間
    /// </summary>
    /// <param name="value">要減少的時間量</param>
    public void ReduceTime(int value)
    {
        currentTime -= value; // 減少當前時間

        // 如果當前時間小於或等於0，則將其設置為0
        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }

    /// <summary>
    /// 重置當前時間為最大時間
    /// </summary>
    public void ResetTime()
    {
        currentTime = maxTime; // 將當前時間設置為最大時間
    }
}
